using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TradingAssistant
{
    internal class MarketOrderingItem
    {
        public required string name;
        public bool enabled;
        public override string ToString() { return name; }
    }
    internal class MarketOrdering : IEnumerable<MarketOrderingItem>
    {
        public void processUpdatedMarketList(HashSet<string> markets)
        {
            removeMissingMarkets(markets);
            foreach (var market in markets)
            {
                addMarket(market);
            }
        }

        // Adds market if not already present, returning true if added.
        // No effect if the market already exists in the list.
        public bool addMarket(string market, bool enabled=true)
        {
            
            if (itemByMarket_.TryGetValue(market, out var item))
            {
                return false;
            } 
            else
            {
                ordering_.Add(new MarketOrderingItem { name =  market, enabled = enabled });
                itemByMarket_.Add(
                    market, ordering_.Last());
                return true;
            }
        }


        // Removes any entries for markets not in `markets`
        public void removeMissingMarkets(HashSet<string> markets)
        {
            List<MarketOrderingItem> newOrdering = new();
            foreach (var  item in ordering_)
            {
                if (markets.Contains(item.name))
                {
                    newOrdering.Add(item);
                }
                else
                {
                    itemByMarket_.Remove(item.name);
                }
            }
            ordering_ = newOrdering;
        }

        public void CopyOrderingFromCollection(ListBox.ObjectCollection objects)
        {
            if (objects.Count != ordering_.Count())
            {
                throw new ArgumentException("objects collection should have the same # of elements as the market list!");
            }
            int count = ordering_.Count();
            for (int i = 0; i < count; i++) {
                var item = objects[i] as MarketOrderingItem;
                if (item == null)
                {
                    throw new ArgumentException("object is not a MarketOrderingItem!");
                }
                ordering_[i] = item;
            }
        }

        public IEnumerator<MarketOrderingItem> GetEnumerator()
        {
            return ((IEnumerable<MarketOrderingItem>)ordering_).GetEnumerator();
        }

        public void saveToDefaultPath()
        {
            saveTo(getDataPath());
        }
        public static MarketOrdering loadFromDefaultPathIfExists()
        {
            if (File.Exists(getDataPath()))
            {
                return MarketOrdering.loadFrom(getDataPath());
            }
            else
            {
                return new MarketOrdering(new List<MarketOrderingItem>());
            }
        }

        private void saveTo(string path)
        {
            File.WriteAllText(
                path, JsonConvert.SerializeObject(ordering_, Globals.serializerSettings));
        }

        private MarketOrdering(List<MarketOrderingItem> ordering)
        {
            ordering_ = ordering;
            foreach (var item in ordering_)
            {
                itemByMarket_.Add(item.name, item);
            }
        }
        private static MarketOrdering loadFrom(string path)
        {
            return new MarketOrdering(JsonConvert.DeserializeObject<List<MarketOrderingItem>>(
                File.ReadAllText(path), Globals.serializerSettings));
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)ordering_).GetEnumerator();
        }

        private List<MarketOrderingItem> ordering_ = new();
        private Dictionary<string, MarketOrderingItem> itemByMarket_ = new();

        private static string getDataPath()
        {
            return Path.Join(Globals.getAppDataFolder(), "market_ordering.json");
        }
    }
}
