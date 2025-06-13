using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TradingAssistant.JsonResponses;

namespace TradingAssistant
{
    internal static class PolygonApi
    {
        static public ThrottledHttpRequester requester { get {
                if (requester_ == null)
                {
                    requester_ = new ThrottledHttpRequester(new ThrottledHttpRequester.Options{
                        minRequestInterval = settings_.polygonThrottlingEnabled ? TimeSpan.FromMinutes(1.0 / settings_.polygonThrottlingQueriesPerMinute) : TimeSpan.Zero,
                    });
                }
                return requester_;
            } }

        static public void updateThrottlingInterval()
        {
            requester.minRequestInterval = settings_.polygonThrottlingEnabled ? TimeSpan.FromMinutes(1.0 / settings_.polygonThrottlingQueriesPerMinute) : TimeSpan.Zero;
        }

        static private HttpRequestMessage buildGetMessage(string uri, Dictionary<string, string> queryParams = null)
        {
            StringBuilder uriBuilder = new(uri);
            if (queryParams != null)
            {
                uriBuilder.Append('?');
                foreach(var entry in queryParams)
                {
                    uriBuilder.AppendFormat("{0}={1}&", HttpUtility.UrlEncode(entry.Key), HttpUtility.UrlEncode(entry.Value));
                }
                uriBuilder.Remove(uriBuilder.Length - 1, 1);
            }
            var message = new HttpRequestMessage(HttpMethod.Get, uriBuilder.ToString());
            message.Headers.Add("Authorization", string.Format("Bearer {0}", apiKey_));
            return message;
        }

        static public bool testApiKey(string apiKey)
        {
            var response = requester.GetAndParseJsonSync<JsonResponses.Dividends>(new HttpRequestMessage(HttpMethod.Get, string.Format("https://api.polygon.io/v3/reference/dividends?apiKey={0}", apiKey)));
            return (response != null && response.status == "OK");
        }

        static public async Task<List<CoinMetadata>> getTickersAsync()
        {
            var queryParams = new Dictionary<string, string> { 
                { "market", "crypto" },
                { "limit", "1000" },
                { "order", "asc" },
                { "sort", "ticker" },
            };
            queryParams["active"] = (!settings_.polygonIgnoreInactiveTickers).ToString();
            Tickers currentBatch = await requester.GetAndParseJsonAsync<Tickers>(buildGetMessage("https://api.polygon.io/v3/reference/tickers", queryParams));
            List<CoinMetadata> result = new();
            result.AddRange(currentBatch.results);
            while (currentBatch.next_url != null)
            {
                currentBatch = await requester.GetAndParseJsonAsync<Tickers>(buildGetMessage(currentBatch.next_url));
                result.AddRange(currentBatch.results);
            }
            return result;
        }

        static private ThrottledHttpRequester requester_ = null;
        static private Properties.Settings settings_ = Properties.Settings.Default;
        static private string apiKey_ { get { return settings_.polygonApiKey; } }
    }
}
