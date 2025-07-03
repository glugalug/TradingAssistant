using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using TradingAssistant.JsonResponses;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TradingAssistant
{
    internal class CoinDeskApi
    {
        static public ThrottledHttpRequester requester { get
            {
                if (requester_ == null)
                {
                    requester_ = new ThrottledHttpRequester(new ThrottledHttpRequester.Options
                    {
                        // For coinDesk, we will auto-adjust throttling based on rate limit headers, but start at 1 QPS.
                        minRequestInterval = TimeSpan.FromSeconds(1),
                    });
                }
                return requester_;
            } 
        }

        // If apiKey is not specified, the apiKey_ member is used.
        static private HttpRequestMessage buildGetMessage(string uri, Dictionary<string, string> queryParams = null, string apiKey = null)
        {
            if (apiKey == null) { apiKey = apiKey_; }
            StringBuilder uriBuilder = new("https://data-api.coindesk.com" + uri);
            if (queryParams != null)
            {
                uriBuilder.Append('?');
                foreach (var entry in queryParams)
                {
                    uriBuilder.AppendFormat("{0}={1}&", HttpUtility.UrlEncode(entry.Key), HttpUtility.UrlEncode(entry.Value));
                }
                uriBuilder.Remove(uriBuilder.Length - 1, 1);
            }
            var message = new HttpRequestMessage(HttpMethod.Get, uriBuilder.ToString());
            message.Headers.Add("X-API-Key", apiKey);
            return message;
        }


        static void ParseRateLimitFromHttpResponseHeaders(HttpResponseMessage response)
        {
            var headers = response.Headers;
            IEnumerable<string> remainingBreakdownHeaders;
            if (headers.TryGetValues("X-RateLimit-Remaining-All", out remainingBreakdownHeaders))
            {
                foreach (var header in remainingBreakdownHeaders)
                {
                    var pieces = header.Split(',');
                    // Ignore response if it isn't the expected format.
                    if (pieces.Length < 2) return;
                    double minRate = 1000.0;
                    foreach (var piece in pieces)
                    {
                        int semiPos = piece.IndexOf(';');
                        if (semiPos < 0) continue;
                        int equalsPos = piece.LastIndexOf('=');
                        if (equalsPos < 0) continue;
                        int windowSize = int.Parse(piece.Substring(equalsPos + 1));
                        // Ignore windows larger than an hour.
                        if (windowSize > 3600) continue;
                        int inWindow = int.Parse(piece.Substring(0, semiPos));
                        double rate = 1.0 * inWindow / windowSize;
                        minRate = Math.Min(minRate, rate);
                    }
                    requester.minRequestInterval = TimeSpan.FromSeconds(1.0 / minRate);
                }
            }
        }

        static private async Task<T?> GetAndParseJsonAsync<T>(
                    HttpRequestMessage message,
                    JsonSerializerSettings? jsonSettings = null)
        {
            return await requester.GetAndParseJsonAsync<T>(message, jsonSettings, ParseRateLimitFromHttpResponseHeaders);
        }

        internal static bool testApiKey(string key)
        {
            var response = requester.GetAndParseJsonSync<JsonResponses.CoinDeskRateLimit>(buildGetMessage("/admin/v2/rate/limit", apiKey: key));
            return (response != null && response?.data?.ApiKey?.REMAINING.minute > 0);
        }

        static public async Task<CoinDeskTopList.TData> getTopListPage(int page)
        {
            HttpRequestMessage request = buildGetMessage('/asset/v1/top/list', new Dictionary<string, string> {
                { "page", page.ToString() },
                {"page_size", "100" },
                {"sort_by", "CREATED_ON" },
                {"sort_direction", "ASC"},
                {"groups", string.Join(',', new string[]{ 
                    "ID", "BASIC", "SUPPORTED_PLATFORMS","CUSTODIANS", "CONTROLLED_ADDRESSES", "SECURITY_METRICS", "SUPPLY",
                    "SUPPLY_ADDRESSES","ASSET_TYPE_SPECIFIC_METRICS","SOCIAL","TOKEN_SALE","EQUITY_SALE","RESOURCE_LINKS",
                    "CLASSIFICATION","PRICE","MKT_CAP","VOLUME","CHANGE","TOPLIST_RANK","DESCRIPTION","DESCRIPTION_SUMMARY",
                    "CONTACT","SEO","INTERNAL"
                }) },
                {"toplist_quote_asset", "USD" }
            }, apiKey_);
            var response = await requester.GetAndParseJsonAsync<CoinDeskTopList>(
                request, jsonSettings: Globals.serializerSettings, preParser: ParseRateLimitFromHttpResponseHeaders);
            if (response?.Err?.type != null)
            {
                throw new ApplicationException(string.Format("Failed to query coindesk toplist: '$0'", response.Err.ToString()));
            }
            return response?.Data;
        }

        static public async Task<CoinDeskTopList.Item[]> getTopList()
        {
            const int kItemsPerPage = 100;
            int page = 1;
            CoinDeskTopList.TData pageResult = await getTopListPage(page);
            int itemCount = pageResult.STATS.TOTAL_ASSETS;
            int pageCount = (itemCount + kItemsPerPage - 1) / kItemsPerPage;
            int elementsAdded = 0;
            var result = new CoinDeskTopList.Item[itemCount];
            Array.Copy(pageResult.LIST, 0, result, 0, pageResult.LIST.Length);
            elementsAdded += pageResult.LIST.Length;
            while (elementsAdded < itemCount)
            {
                pageResult = await getTopListPage(++page);
                Array.Copy(pageResult.LIST, 0, result, elementsAdded, pageResult.LIST.Length);
                elementsAdded += pageResult.LIST.Length;
            }
            return result;
        }

        static private ThrottledHttpRequester requester_ = null;
        static private Properties.Settings settings_ = Properties.Settings.Default;
        static private string apiKey_ { get { return settings_.polygonApiKey; } }
    }
}
