using RestSharp.Deserializers;

namespace SecAnalyzer.DTOs
{
    public class YearlyEnterpriseValue
    {
        [DeserializeAs(Name = "date")]
        public string Date { get; set; }

        [DeserializeAs(Name = "marketCapitalization")]
        public long MarketCap { get; set; }

        [DeserializeAs(Name = "enterpriseValue")]
        public long EnterpriseValue { get; set; }
    }
}
