using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TradingAssistant.JsonResponses
{
    internal class Tickers
    {
        public int? count;
        // if present, this should fetch the next page
        public string? next_url;
        public string? request_id;
        public CoinMetadata[] results;
        public string? status;
    }

    public class CoinMetadata
    {
        // This is not present in polygon docs, but is in the response and seems more relevant than the fields that are.
        // Example: XCN
        [Display(AutoGenerateField = true, Name = "token ticker")]
        public string? base_currency_symbol { get; set; }
        // This is not present in polygon docs, but is in the response and seems more relevant than the fields that are.
        // Ex OnyxCoin
        [Display(Name = "token name")]
        public string? base_currency_name { get; set; }

        [Display(Name = "primary exchange")]
        public string? primary_exchange { get; set; }
        [Display(Name = "last updated (UTC)")]
        DateTime? last_updated_utc { get; set; }

        [Display(Name = "delisted (UTC)")]
        DateTime? delisted_utc { get; set; }
        public enum Locale
        {
            us,
            global
        }
        public Locale locale { get; set; }
        public enum Market
        {
            stocks,
            crypto,
            fix,
            otc,
            indices
        }
        public Market market {  get; set; }

        // Symbol for the currency the token is traded in, usually `USD`.
        [Display(Name = "currency")]
        public string? currency_symbol { get; set; }
        // This is not present in polygon docs, but is in the response and seems more relevant than the fields that are.
        // Name of the currency the token is traded in, usually `United States Dollar`.
        [Display(Name = "currency name")]
        public string? currency_name { get; set; }

        // Long format name, ex: "OnyxCoin - United States Dollar"
        [Display(Name = "Polygon name")]
        public string name { get; set; }

        // Unique ticker used by Polygon. Usually of the form 'X:<CRYPTO TICKER><CURRENCY>' ex `X:BTCUSD`
        [Display(AutoGenerateField = true, Name = "Polygon Ticker")]
        public string ticker { get; set; }

        [Display(AutoGenerateField = false)]
        bool? active { get; set; }
        // SEC CIK code.  Not present for crypto, not generally interesting for stocks.
        [Display(AutoGenerateField = false)]
        string? cik {  get; set; }
        // Separate standard similar to CIK, not *usually* present for crypto
        [Display(AutoGenerateField = false)]
        string? composite_figi {  get; set; }
        // Not usually present for crypto or interesting otherwise.
        [Display(AutoGenerateField = false)]
        public string? share_class_figi { get; set; }
        // Type of asset.  Not usually present, and if it were, should always be "crypto".
        [Display(AutoGenerateField = false)]
        public string? type {  get; set; }
    }
}
