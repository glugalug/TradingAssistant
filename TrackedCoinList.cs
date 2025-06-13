using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.VisualBasic;
using TradingAssistant.DataFetchers;
using TradingAssistant.JsonResponses;

namespace TradingAssistant
{
    internal class TrackedCoinList
    {
        public static TrackedCoinList instance = null;

        public HashSet<string> getCoinsMissingStats()
        {
            HashSet<string> missing = new HashSet<string>();
            foreach (string id in trackedCoinProperties_.Keys)
            {
                if (refresher_.TryGetByKey(id) == null)
                {
                    missing.Add(id);
                }
            }
            return missing;
        }

        public void removeCoinIds(IEnumerable<string> ids)
        {
            foreach (var id in ids) removeCoinId(id);
        }
        public void removeCoinId(string id)
        {
            if (trackedCoinProperties_.TryGetValue(id, out CoinProperties coinProperties))
            {
                if (coinProperties.metadata != null)
                {
                    trackedTickers_.Remove(coinProperties.metadata);
                }
                trackedCoinNames_.Remove(coinProperties);
                trackedCoinProperties_.Remove(id);
                OnCoinRemoved?.Invoke(id);
            }
        }
        public void addCoinId(string id)
        {
            if (trackedCoinProperties_.ContainsKey(id)) return;
            CoinMetadata? stats = refresher_.TryGetByKey(id);
            var props = new CoinProperties(id, stats);
            trackedCoinProperties_.Add(id, props);
            if (stats != null)
            {
                trackedTickers_.Add(stats);
                trackedCoinNames_.Add(props);
            }
            OnCoinAdded?.Invoke(id);
        }

        public delegate void CoinEvent(string coinId);
        public event CoinEvent OnCoinAdded;
        public event CoinEvent OnCoinRemoved;

        public bool includes(string id) { return trackedCoinProperties_.ContainsKey(id); }

        public SortableBindingList<CoinMetadata> coinMetadataBinding { get { return trackedTickers_; } }
        public SortableBindingList<CoinProperties> coinNamesBinding { get { return trackedCoinNames_; } }
        private SortableBindingList<CoinMetadata> trackedTickers_ = new SortableBindingList<CoinMetadata>(new List<CoinMetadata>());
        private SortableBindingList<CoinProperties> trackedCoinNames_ = new SortableBindingList<CoinProperties>(new List<CoinProperties>());

        public class CoinProperties
        {
            public CoinProperties(string ticker, CoinMetadata? metadata)
            {
                this.ticker = ticker;
                this.metadata = metadata;
                if (metadata == null)
                {
                    comboSelectionName = string.Format("{0} with no info available", ticker);
                }
                else
                {
                    comboSelectionName = string.Format("${0} ({1}) - {2}", metadata.base_currency_symbol, ticker, metadata.name);
                }
            }
            public string toString() { return comboSelectionName; }
            public CoinMetadata? metadata;
            public string comboSelectionName;
            public string name { get { return comboSelectionName; } }
            public string ticker;
        }

        private void rebuildBindingLists()
        {
            trackedTickers_.RaiseListChangedEvents = false;
            trackedCoinNames_.RaiseListChangedEvents = false;
            try
            {
                foreach (var entry in trackedCoinProperties_)
                {
                    var key = entry.Key;
                    CoinMetadata? metadata = refresher_.TryGetByKey(key);
                    CoinProperties props = entry.Value;
                    CoinMetadata? oldMetadata = props.metadata;
                    if (metadata == oldMetadata) continue;
                    if (metadata != null)
                    {
                        props.metadata = metadata;
                        if (oldMetadata != null)
                        {
                            trackedTickers_.Remove(oldMetadata);
                        }
                        trackedTickers_.Add(metadata);
                    }
                }
                trackedCoinNames_.ResetBindings();
            }
            finally
            {
                trackedTickers_.RaiseListChangedEvents = true;
                trackedCoinNames_.RaiseListChangedEvents = true;
            }
        }

        public static TrackedCoinList loadFromDefaultLocation(DataFetchers.CoinMetadataRefresher refresher)
        {
            string path = getDefaultLocation();
            if (!File.Exists(path)) return new TrackedCoinList(new List<string>(), refresher);
            return loadFrom(path, refresher);
        }

        public static TrackedCoinList loadFrom(string path, DataFetchers.CoinMetadataRefresher refresher)
        {
            Console.WriteLine("loading tracked coin list from " + path);
            return new TrackedCoinList(File.ReadAllLines(path), refresher);
        }

        private TrackedCoinList(IEnumerable<string> coins, DataFetchers.CoinMetadataRefresher refresher)
        {
            refresher_ = refresher;
            refresher.bindingList.ListChanged += Refresher_ListChanged;
            foreach (string coin in coins)
            {
                if (coin.Length > 0)
                {
                    addCoinId(coin);
                }
            }
        }

        private void Refresher_ListChanged(object? sender, ListChangedEventArgs e)
        {
            rebuildBindingLists();
        }

        private static string getDefaultLocation()
        {
            return Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "trackedCoins.txt");
        }

        public void saveToDefaultLocation()
        {
            saveTo(getDefaultLocation());
        }

        public void saveTo(string path)
        {
            Console.WriteLine("Saving tracked coin stats to: " + path);
            File.WriteAllLines(path, trackedCoinProperties_.Keys);
        }

        private Dictionary<string, CoinProperties> trackedCoinProperties_ = new();
        private DataFetchers.CoinMetadataRefresher refresher_;

        // TODO: Add rebuild properties & bindings functionality for stats reload.
    }
}
