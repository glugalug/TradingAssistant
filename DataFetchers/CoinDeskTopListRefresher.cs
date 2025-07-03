using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingAssistant.JsonResponses;

namespace TradingAssistant.DataFetchers
{
    internal class CoinDeskTopListRefresher : RefreshableDataRecords<CoinDeskTopList.Item, int>

    {
        public CoinDeskTopListRefresher(RefreshOptions options) : base(options)
        {
            Init();
        }

        protected override string getDatasetName()
        {
            return "coindesk_toplist";
        }

        protected override int getPrimaryKey(CoinDeskTopList.Item record)
        {
            return record.ID;
        }

        protected override RefreshOffsetEnum getRefreshOffsetKind()
        {
            return RefreshOffsetEnum.UTCday;
        }

        protected override async Task<List<CoinDeskTopList.Item>> startFetch()
        {
            return (await CoinDeskApi.getTopList()).ToList();
        }
    }
}
