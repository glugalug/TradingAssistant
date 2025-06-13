using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingAssistant.JsonResponses
{
    internal class Dividends
    {
        public class Result
        {
            public double cash_amount;
            public string currency;
            public DateTime declaration_date;
            public string dividend_type;
            public DateTime ex_dividend_date;
            public int frequency;
            public string id;
            public DateTime pay_date;
            public DateTime record_date;
            public string ticker;
        }
        public Result[] results;
        public string status;
        public string request_id;
    }
}
