using Microsoft.EntityFrameworkCore.Migrations;

namespace SecAnalyzer.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Config",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Config", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Symbol = table.Column<string>(nullable: true),
                    Year = table.Column<int>(nullable: false),
                    Revenue = table.Column<long>(nullable: false),
                    CostOfRevenue = table.Column<long>(nullable: false),
                    ResearchAndDevelopmentCosts = table.Column<long>(nullable: false),
                    SellingGeneralAndAdministrativeCosts = table.Column<long>(nullable: false),
                    DepreciationAndAmortization = table.Column<long>(nullable: false),
                    NetIncome = table.Column<long>(nullable: false),
                    MarketCap = table.Column<long>(nullable: false),
                    TotalDebt = table.Column<long>(nullable: false),
                    Cash = table.Column<long>(nullable: false),
                    NetPropertyPlantAndEquipment = table.Column<long>(nullable: false),
                    TotalCurrentAssets = table.Column<long>(nullable: false),
                    RevenuePerShare = table.Column<decimal>(nullable: false),
                    NetIncomePerShare = table.Column<decimal>(nullable: false),
                    FreeCashFlowPerShare = table.Column<decimal>(nullable: false),
                    TangibleBookValuePerShare = table.Column<decimal>(nullable: false),
                    EnterpriseValue = table.Column<long>(nullable: false),
                    PERatio = table.Column<decimal>(nullable: false),
                    PriceToFreeCashFlowRatio = table.Column<decimal>(nullable: false),
                    PriteToTangibleBookRatio = table.Column<decimal>(nullable: false),
                    EarningsYield = table.Column<decimal>(nullable: false),
                    ReturnOnTangibleAssets = table.Column<decimal>(nullable: false),
                    GrahamNetNet = table.Column<decimal>(nullable: false),
                    YearlyHighSharePrice = table.Column<decimal>(nullable: false),
                    YearlyLowSharePrice = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Config");

            migrationBuilder.DropTable(
                name: "Stocks");
        }
    }
}
