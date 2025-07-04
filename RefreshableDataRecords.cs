using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.Design.AxImporter;

namespace TradingAssistant
{
    public class RefreshOptions
    {
        public bool refreshEnabled { get; set; }
        public TimeSpan refreshInterval { get; set; }
        public TimeSpan? refreshOffset { get; set; }

        [DefaultValue(true)]
        public bool constructorWaitsForFirstLoad { get; set; }

        public bool removeMissingElementsAfterReload { get; set; }
    }

    internal abstract class RefreshableDataRecordsInterface : IDisposable
    {
        public static void disposeAllRefreshers()
        {
            foreach (var refresher in refreshers_)
            {
                refresher.Key.getDisposable().Dispose();
            }
        }
        // A list of all refreshers in existence, to facilitate making sure they are all stopped during shutdown.
        private static ConcurrentDictionary<RefreshableDataRecordsInterface, bool> refreshers_ = new();

        public RefreshableDataRecordsInterface()
        {
            refreshers_[this] = true;
        }

        public void UpdateRefreshOptions(RefreshOptions refreshOptions)
        {
            options_ = refreshOptions;
        }
        public bool refreshEnabled
        {
            get { return options.refreshEnabled; }
            set { options.refreshEnabled = value; updateRefreshCycle(); }
        }
        public TimeSpan refreshInterval
        {
            get { return options.refreshInterval; }
            set { options.refreshInterval = value; updateRefreshCycle(); }
        }
        public TimeSpan? refreshOffset
        {
            get { return options.refreshOffset; }
            set { options.refreshOffset = value; updateRefreshCycle(); }
        }
        protected enum RefreshOffsetEnum
        {
            None = 0,
            Hour = 1,
            UTCday = 2,
            Custom = 3
        }

        public RefreshOptions options
        {
            get { return options_; }
            set
            {
                if (options_ != value)
                {
                    options_ = value;
                    updateRefreshCycle();
                }
            }
        }

        protected RefreshOptions options_;
        protected void updateRefreshCycle()
        {
            lock (shutdownLock_)
            {
                if (refreshInProgress_) return;
                if (refreshTimer_ != null)
                {
                    refreshTimer_.Dispose();
                    refreshTimer_ = null;
                }
            }
            if (refreshEnabled)
            {
                DateTime nextUpdate = (lastUpdate_ == null) ? DateTime.UtcNow : applyRefreshOffset((DateTime)lastUpdate_);
                long waitMs = (long)nextUpdate.Subtract(DateTime.Now).TotalMilliseconds;
                // If the update is overdue, schedule it immediately.
                if (waitMs < 0) waitMs = 0;
                refreshTimer_ = new System.Threading.Timer(refreshTimerCallback, this, waitMs, Timeout.Infinite);
            }
        }
        protected System.Threading.Timer? refreshTimer_ = null;


        private void notifyShuttingDown()
        {
            refreshTimer_?.Dispose();
            refreshTimer_ = null;
        }
        public DateTime? lastUpdate { get { return lastUpdate_; } }
        protected DateTime? lastUpdate_;

        private DateTime applyRefreshOffset(DateTime organicTime)
        {
            if (refreshOffset == null) return organicTime.Add(refreshInterval);
            switch (getRefreshOffsetKind())
            {
                case RefreshOffsetEnum.None: return organicTime;
                case RefreshOffsetEnum.Hour:
                    // subtract offset time, then truncate before adding interval and re-adding offset.
                    organicTime = organicTime.Subtract((TimeSpan)refreshOffset);
                    return organicTime.Date.AddHours(organicTime.TimeOfDay.Hours).Add(refreshInterval + (TimeSpan)refreshOffset);
                case RefreshOffsetEnum.UTCday:
                    // subtract offset, truncate to day then add interval + offset.
                    return organicTime.ToUniversalTime().Subtract((TimeSpan)refreshOffset).Date.Add(refreshInterval + (TimeSpan)refreshOffset);
                case RefreshOffsetEnum.Custom:
                    return applyCustomOffset(organicTime);
            }
            throw new ArgumentException("Unrecognized refresh offset!");
        }

        abstract protected RefreshOffsetEnum getRefreshOffsetKind();
        protected virtual DateTime applyCustomOffset(DateTime organicTime)
        {
            throw new NotImplementedException();
        }
        private void refreshTimerCallback(object? state)
        {
            Task.Run(async () => await startRefresh());
        }

        public abstract Task startRefresh();
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                lock (shutdownLock_)
                {
                    shuttingDown_ = true;
                    notifyShuttingDown();
                    while (refreshInProgress_)
                    {
                        Monitor.Wait(shutdownLock_);
                    }
                }
                refreshTimer_?.Dispose();
                // TODO: dispose managed state (managed objects)
                bool temp;
                refreshers_.TryRemove(this, out temp);

                if (disposing)
                {
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }
        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~RefreshableDataRecords()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        private IDisposable getDisposable() { return this; }

        private bool disposedValue;
        protected object shutdownLock_ = new();
        protected bool shuttingDown_ = false;
        protected bool refreshInProgress_ = false;
    }

    internal abstract class RefreshableDataRecords<T, K> : RefreshableDataRecordsInterface
         where T : notnull where K : notnull
    {
        public Type KeyType = typeof(K);
        public RefreshableDataRecords(RefreshOptions options)
        {
            options_ = options;
        }

        protected void Init()
        {
            inited_ = true;
            if (!Directory.Exists(getDataFolder()))
            {
                Directory.CreateDirectory(getDataFolder());
            }
            List<T> data = new List<T>();
            try
            {
                data = loadFromFile(getDataPath());
                foreach (T item in data)
                {
                    index_.Add(getPrimaryKey(item), item);
                }
                loadLastUpdated();
            }
            catch (Exception ex) { }
            bindingList_ = new(data);
            // If data has never been load it, try to do an initial load.
            if (lastUpdate_ == null)
            {
                Task.Run(async () => startRefresh());
                if (options_.constructorWaitsForFirstLoad)
                {
                    loadDoneEvent_.WaitOne();
                }
            }
            updateRefreshCycle();
        }
        ManualResetEvent loadDoneEvent_ = new(false);

        private List<T> loadFromFile(string path)
        {
            return JsonConvert.DeserializeObject<List<T>>(File.ReadAllText(path), Globals.serializerSettings);
        }
        private void saveToFile(string path)
        {
            File.WriteAllText(path, JsonConvert.SerializeObject(bindingList, Globals.serializerSettings));
        }
        private void saveToFile()
        {
            saveToFile(getDataPath());
        }
        protected abstract string getDatasetName();
        protected abstract Task<List<T>> startFetch();

        protected abstract K getPrimaryKey(T record);

        public T? TryGetByKey(K key)
        {
            T? result;
            index_.TryGetValue(key, out result);
            return result;
        }

        private string getDataFolder()
        {
            string path = Path.Join(
                 Globals.getAppDataFolder(), getDatasetName());

            return path;
        }
        private string getDataPath()
        {
            Console.WriteLine("data folder:" + getDataFolder());
            return Path.Join(getDataFolder(), "raw_data.json");
        }
        private string getLastUpdatedPath()
        {
            return Path.Join(getDataFolder(), "last_updated.txt");
        }
        private void saveLastUpdated()
        {
            File.WriteAllText(getLastUpdatedPath(), lastUpdate_?.ToUniversalTime().ToString());
        }
        private void loadLastUpdated()
        {
            string contents = File.ReadAllText(getLastUpdatedPath());
            lastUpdate_ = DateTime.Parse(contents);
        }

        public SortableBindingList<T> bindingList { get { return bindingList_; } }

        public sealed override async Task startRefresh()
        {
            if (!inited_)
            {
                throw new InvalidOperationException("Not initialized, make sure subclass constructor calls init!");
            }
            lock (shutdownLock_)
            {
                refreshInProgress_ = true;
            }
            // If there is a timer scheduled, unschedule it while we run this refresh.
            refreshTimer_?.Dispose();
            refreshTimer_ = null;
            // lastUpdate_ is updated *before* the attempt, so it counts for the current
            // process *even if it failed*. Otherwise there could be an endless loop of failures
            // from a bad API key for example.
            lastUpdate_ = DateTime.Now;
            try
            {
                List<T> new_data = await startFetch();
                mergeData(new_data);
                saveToFile();
                saveLastUpdated();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                lock (shutdownLock_)
                {
                    refreshInProgress_ = false;
                    Monitor.Pulse(shutdownLock_);
                    if (!shuttingDown_)
                    {
                        // Schedule the next refresh even if the previous one failed!
                        updateRefreshCycle();
                    }
                }
                loadDoneEvent_.Set();
            }
        }

        // By default, the "new" record is kept as is.
        // Subclasses can override this for a smarter merge.
        protected virtual T mergeRecords(T oldData, T newData)
        {
            return newData;
        }
        private void mergeData(List<T> new_data)
        {
            bindingList_.RaiseListChangedEvents = false;
            HashSet<K> unseen_keys = new(index_.Keys);
            foreach (T rec in new_data)
            {
                K key = getPrimaryKey(rec);
                unseen_keys.Remove(key);
                T old_val;
                var merged = rec;
                if (index_.TryGetValue(key, out old_val))
                {
                    if (old_val.Equals(new_data))
                    {
                        continue;
                    }
                    else
                    {
                        bindingList_.Remove(old_val);
                    }
                    merged = mergeRecords(old_val, rec);
                }
                index_[key] = merged;
                bindingList_.Add(merged);
            }
            if (options.removeMissingElementsAfterReload)
            {
                foreach (K key in unseen_keys)
                {
                    T old_val = index_[key];
                    index_.Remove(key);
                    bindingList_.Remove(old_val);
                }
            }

            bindingList_.RaiseListChangedEvents = true;
            OnDataReloaded?.Invoke(this, new DataReloadedEventArgs(bindingList_));
        }

        public class DataReloadedEventArgs
        {
            internal DataReloadedEventArgs(IBindingList ibl)
            {
                bindingList = ibl;
            }
            public IBindingList bindingList;
        }
        public delegate void DataReloadedDelegate(RefreshableDataRecordsInterface sender, DataReloadedEventArgs args);
        public event DataReloadedDelegate OnDataReloaded;

        // Calls ResetBindings on the UI thread using the Invoke method of the supplied control.
        // Crashes happen from the bindings being used as a DataGridView datasource if this is done on other threads, so
        // it's best to use the OnDataReload event to marshal it in.
        public void ResetBindingsOnControlThread(Control ctrl)
        {
            ctrl.Invoke((MethodInvoker)delegate
            {
                bindingList.ResetBindings();
            });
        }

        private SortableBindingList<T> bindingList_ = new SortableBindingList<T>(new List<T>());
        private Dictionary<K, T> index_ = new();
        private bool inited_ = false;
    }
}
