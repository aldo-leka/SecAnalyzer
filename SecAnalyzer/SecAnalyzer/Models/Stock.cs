using System.ComponentModel.DataAnnotations.Schema;

namespace SecAnalyzer.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public int Year { get; set; }

        public long Revenue { get; set; }
        public long CostOfRevenue { get; set; }
        public long ResearchAndDevelopmentCosts { get; set; }
        public long SellingGeneralAndAdministrativeCosts { get; set; }
        public long DepreciationAndAmortization { get; set; }
        public long NetIncome { get; set; }

        public long MarketCap { get; set; }
        public long TotalDebt { get; set; }
        public long Cash { get; set; }
        public long NetPropertyPlantAndEquipment { get; set; }
        public long TotalCurrentAssets { get; set; }

        public decimal RevenuePerShare { get; set; }
        public decimal NetIncomePerShare { get; set; }
        public decimal FreeCashFlowPerShare { get; set; }
        public decimal TangibleBookValuePerShare { get; set; }
        public long EnterpriseValue { get; set; }
        public decimal PERatio { get; set; }
        public decimal PriceToFreeCashFlowRatio { get; set; }
        public decimal PriteToTangibleBookRatio { get; set; }
        public decimal EarningsYield { get; set; }
        public decimal ReturnOnTangibleAssets { get; set; }
        public decimal GrahamNetNet { get; set; }

        public decimal YearlyHighSharePrice { get; set; }
        public decimal YearlyLowSharePrice { get; set; }
        public decimal YearlyStartSharePrice { get; set; }
        public decimal YearlyEndSharePrice { get; set; }
    }
}
