using RestSharp.Deserializers;

namespace SecAnalyzer.DTOs
{
    public class YearlyBalanceSheet
    {
        [DeserializeAs(Name = "symbol")]
        public string Symbol { get; set; }

        [DeserializeAs(Name = "fillingDate")]
        public string FilingDate { get; set; }

        [DeserializeAs(Name = "acceptedDate")]
        public string AcceptedDate { get; set; }

        //SHORT-TERM ASSETS

        [DeserializeAs(Name = "cashAndShortTermInvestments")]
        public long CashAndShortTermInvestments { get; set; }

        [DeserializeAs(Name = "netReceivables")]
        public long NetReceivables { get; set; }

        [DeserializeAs(Name = "inventory")]
        public long Inventory { get; set; }

        [DeserializeAs(Name = "otherCurrentAssets")]
        public long OtherCurrentAssets { get; set; }

        [DeserializeAs(Name = "totalCurrentAssets")]
        public long TotalCurrentAssets { get; set; }

        //LONG-TERM ASSETS

        [DeserializeAs(Name = "propertyPlantEquipmentNet")]
        public long NetPropertyPlantAndEquipment { get; set; }

        [DeserializeAs(Name = "goodwillAndIntangibleAssets")]
        public long GoodwillAndIntangibleAssets { get; set; }

        [DeserializeAs(Name = "longTermInvestments")]
        public long LongTermInvestments { get; set; }

        [DeserializeAs(Name = "otherNonCurrentAssets")]
        public long OtherNonCurrentAssets { get; set; }

        [DeserializeAs(Name = "totalNonCurrentAssets")]
        public long TotalNonCurrentAssets { get; set; }

        //SHORT-TERM LIABILITIES

        [DeserializeAs(Name = "accountPayables")]
        public long AccountPayables { get; set; }

        [DeserializeAs(Name = "shortTermDebt")]
        public long ShortTermDebt { get; set; }

        [DeserializeAs(Name = "totalCurrentLiabilities")]
        public long TotalCurrentLiabilities { get; set; }

        //LONG-TERM LIABILITIES

        [DeserializeAs(Name = "longTermDebt")]
        public long LongTermDebt { get; set; }

        [DeserializeAs(Name = "totalNonCurrentLiabilities")]
        public long TotalNonCurrentLiabilities { get; set; }

        [DeserializeAs(Name = "totalLiabilities")]
        public long TotalLiabilities { get; set; }
    }
}
