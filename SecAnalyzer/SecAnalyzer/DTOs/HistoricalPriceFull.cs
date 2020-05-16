using RestSharp.Deserializers;
using System.Collections.Generic;

namespace SecAnalyzer.DTOs
{
    public class HistoricalPriceFull
    {
        [DeserializeAs(Name = "symbol")]
        public string Symbol { get; set; }

        [DeserializeAs(Name = "historical")]
        public List<HistoricalPrice> DailyPrices { get; set; }
    }
}
