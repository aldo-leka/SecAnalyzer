using RestSharp.Deserializers;

namespace SecAnalyzer.DTOs
{
    public class YearlyIncomeStatement
    {
        [DeserializeAs(Name = "symbol")]
        public string Symbol { get; set; }

        [DeserializeAs(Name = "fillingDate")]
        public string FilingDate { get; set; }

        [DeserializeAs(Name = "acceptedDate")]
        public string AcceptedDate { get; set; }

        [DeserializeAs(Name = "revenue")]
        public long Revenue { get; set; }

        [DeserializeAs(Name = "costOfRevenue")]
        public long CostOfRevenue { get; set; }

        [DeserializeAs(Name = "researchAndDevelopmentExpenses")]
        public long ResearchAndDevelopmentExpenses { get; set; }

        [DeserializeAs(Name = "generalAndAdministrativeExpenses")]
        public long GeneralAndAdministrativeExpenses { get; set; }

        [DeserializeAs(Name = "sellingAndMarketingExpenses")]
        public long SellingAndMarketingExpenses { get; set; }

        [DeserializeAs(Name = "interestExpense")]
        public long InterestExpense { get; set; }

        [DeserializeAs(Name = "depreciationAndAmortization")]
        public long DepreciationAndAmortization { get; set; }

        [DeserializeAs(Name = "netIncome")]
        public long NetIncome { get; set; }
    }
}
