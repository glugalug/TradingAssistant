namespace TradingAssistant.JsonResponses
{
    internal class CoinDeskRateLimit
    {
        public class RateLimitBreakdownCounts
        {
            public int? month { get; set; }
            public int? day { get; set; }
            public int? hour { get; set; }
            public int? minute { get; set; }
            public int? second { get; set; }
            public int? lifetime { get; set; }
            public double? soft_cap_allowance_multiplier { get; set; }
        }
        public class KeyUsageStats
        {
            public RateLimitBreakdownCounts USED { get; set; }
            public RateLimitBreakdownCounts MAX { get; set; }
            public RateLimitBreakdownCounts REMAINING { get; set; }
        }

        public class RateLimitData
        {
            public KeyUsageStats? ApiKey { get; set; }
            public KeyUsageStats? AuthKey { get; set; }
        }
        public RateLimitData? data { get; set; }

        public class ErrorInfo { }
        public ErrorInfo? Err {  get; set; }
    }
}