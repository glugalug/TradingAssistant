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

        public HashSet<int> getCoinsMissingStats()
        {
            var missing = new HashSet<int>();
            foreach (var id in trackedCoinProperties_.Keys)
            {
                if (refresher_.TryGetByKey(id) == null)
                {
                    missing.Add(id);
                }
            }
            return missing;
        }

        public void removeCoinIds(IEnumerable<int> ids)
        {
            foreach (var id in ids) removeCoinId(id);
        }
        public void removeCoinId(int id)
        {
            if (trackedCoinProperties_.TryGetValue(id, out CoinProperties coinProperties))
            {
                if (coinProperties.item != null)
                {
                    trackedTickers_.Remove(coinProperties.item);
                }
                trackedCoinNames_.Remove(coinProperties);
                trackedCoinProperties_.Remove(id);
                OnCoinRemoved?.Invoke(id);
            }
            saveToDefaultLocation();
        }
        public void addCoinId(int id)
        {
            if (trackedCoinProperties_.ContainsKey(id)) return;
            CoinDeskTopList.Item? item = refresher_.TryGetByKey(id);
            if (item != null)
            {
                var props = new CoinProperties(id, item);
                trackedCoinProperties_.Add(id, props);
                trackedTickers_.Add(item);
                trackedCoinNames_.Add(props);
            }
            OnCoinAdded?.Invoke(id);
            saveToDefaultLocation();
        }

        public delegate void CoinEvent(int coinId);
        public event CoinEvent OnCoinAdded;
        public event CoinEvent OnCoinRemoved;

        public bool includes(int id) { return trackedCoinProperties_.ContainsKey(id); }

        public SortableBindingList<CoinDeskTopList.Item> coinMetadataBinding { get { return trackedTickers_; } }
        public SortableBindingList<CoinProperties> coinNamesBinding { get { return trackedCoinNames_; } }
        private SortableBindingList<CoinDeskTopList.Item> trackedTickers_ = new SortableBindingList<CoinDeskTopList.Item>(new List<CoinDeskTopList.Item>());
        private SortableBindingList<CoinProperties> trackedCoinNames_ = new SortableBindingList<CoinProperties>(new List<CoinProperties>());

        public class CoinProperties
        {
            public CoinProperties(int ID, CoinDeskTopList.Item? item)
            {
                this.ID = ID;
                this.item = item;
                if (item == null)
                {
                    comboSelectionName = string.Format("Coin ID {0} with no info available", ID);
                }
                else
                {
                    comboSelectionName = string.Format("${0} ({1}) - {2}", ID, item.SYMBOL, item.NAME);
                }
            }
            public string toString() { return comboSelectionName; }
            public CoinDeskTopList.Item? item;
            public string comboSelectionName;
            public string name { get { return comboSelectionName; } }
            public int ID;
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
                    CoinDeskTopList.Item? item = refresher_.TryGetByKey(key);
                    CoinProperties props = entry.Value;
                    CoinDeskTopList.Item? oldItem = props.item;
                    if (item == oldItem) continue;
                    if (item != null)
                    {
                        props.item = item;
                        if (oldItem != null)
                        {
                            trackedTickers_.Remove(oldItem);
                        }
                        trackedTickers_.Add(item);
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

        public static TrackedCoinList loadFromDefaultLocation(DataFetchers.CoinDeskTopListRefresher refresher)
        {
            string path = getDefaultLocation();
            if (!File.Exists(path)) return new TrackedCoinList(new List<int>(), refresher);
            return loadFrom(path, refresher);
        }

        public static TrackedCoinList loadFrom(string path, DataFetchers.CoinDeskTopListRefresher refresher)
        {
            Console.WriteLine("loading tracked coin list from " + path);
            return new TrackedCoinList(File.ReadAllLines(path).ToList().ConvertAll(s => int.Parse(s)), refresher);
        }

        private TrackedCoinList(IEnumerable<int> coins, DataFetchers.CoinDeskTopListRefresher refresher)
        {
            refresher_ = refresher;
            refresher.bindingList.ListChanged += Refresher_ListChanged;
            foreach (int coin in coins)
            {
                addCoinId(coin);
            }
        }

        private void Refresher_ListChanged(object? sender, ListChangedEventArgs e)
        {
            rebuildBindingLists();
        }

        private static string getDefaultLocation()
        {
            return Path.Join(Globals.getAppDataFolder(), "trackedCoins.txt");
        }

        public void saveToDefaultLocation()
        {
            saveTo(getDefaultLocation());
        }

        public void saveTo(string path)
        {
            Console.WriteLine("Saving tracked coin stats to: " + path);
            File.WriteAllLines(path, trackedCoinProperties_.Keys.ToList().ConvertAll(i => i.ToString()));
        }

        private Dictionary<int, CoinProperties> trackedCoinProperties_ = new();
        private DataFetchers.CoinDeskTopListRefresher refresher_;

        // TODO: Add rebuild properties & bindings functionality for stats reload.
    }
}
