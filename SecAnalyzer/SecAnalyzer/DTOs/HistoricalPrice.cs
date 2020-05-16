using RestSharp.Deserializers;

namespace SecAnalyzer.DTOs
{
    public class HistoricalPrice
    {
        [DeserializeAs(Name = "date")]
        public string Date { get; set; }

        [DeserializeAs(Name = "close")]
        public decimal Price { get; set; }
    }
}
