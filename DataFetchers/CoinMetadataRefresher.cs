using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TradingAssistant.JsonResponses;

namespace TradingAssistant.DataFetchers
{
    internal class CoinMetadataRefresher : RefreshableDataRecords<CoinMetadata, string>
    {
        public CoinMetadataRefresher(RefreshOptions options) : base(options)
        {
            Init();
        }

        protected override string getDatasetName()
        {
            return "polygon_tickers";
        }

        protected override string getPrimaryKey(CoinMetadata record)
        {
            return record.ticker;
        }

        protected override RefreshOffsetEnum getRefreshOffsetKind()
        {
            return RefreshOffsetEnum.UTCday;
        }

        protected override async Task<List<CoinMetadata>> startFetch()
        {
            return await PolygonApi.getTickersAsync();
        }
    }
}
