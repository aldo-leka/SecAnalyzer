using RestSharp.Deserializers;

namespace SecAnalyzer.DTOs
{
    public class YearlyKeyMetrics
    {
        [DeserializeAs(Name = "symbol")]
        public string Symbol { get; set; }

        [DeserializeAs(Name = "date")]
        public string Date { get; set; }

        [DeserializeAs(Name = "revenuePerShare")]
        public decimal RevenuePerShare { get; set; }

        [DeserializeAs(Name = "netIncomePerShare")]
        public decimal NetIncomePerShare { get; set; }

        [DeserializeAs(Name = "freeCashFlowPerShare")]
        public decimal FreeCashFlowPerShare { get; set; }

        [DeserializeAs(Name = "cashPerShare")]
        public decimal CashPerShare { get; set; }

        [DeserializeAs(Name = "tangibleBookValuePerShare")]
        public decimal TangibleBookValuePerShare { get; set; }

        [DeserializeAs(Name = "marketCap")]
        public long MarketCap { get; set; }

        [DeserializeAs(Name = "enterpriseValue")]
        public long EnterpriseValue { get; set; }

        [DeserializeAs(Name = "peRatio")]
        public decimal PERatio { get; set; }

        [DeserializeAs(Name = "pfcfRatio")]
        public decimal PriceToFreeCashFlowRatio { get; set; }

        [DeserializeAs(Name = "ptbRatio")]
        public decimal PriteToTangibleBookRatio { get; set; }

        [DeserializeAs(Name = "evToFreeCashFlow")]
        public decimal EnterpriseValueToFreeCashFlow { get; set; }

        [DeserializeAs(Name = "earningsYield")]
        public decimal EarningsYield { get; set; }

        [DeserializeAs(Name = "freeCashFlowYield")]
        public decimal FreeCashFlowYield { get; set; }
        
        [DeserializeAs(Name = "roic")]
        public decimal ReturnOnInvestedCapital { get; set; }

        [DeserializeAs(Name = "returnOnTangibleAssets")]
        public decimal ReturnOnTangibleAssets { get; set; }

        [DeserializeAs(Name = "grahamNetNet")]
        public decimal GrahamNetNet { get; set; }

        [DeserializeAs(Name = "capexPerShare")]
        public decimal CapexPerShare { get; set; }
    }
}
