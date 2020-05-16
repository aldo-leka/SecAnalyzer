using Microsoft.EntityFrameworkCore.Migrations;

namespace SecAnalyzer.Migrations
{
    public partial class YearlyStartEndSharePrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "YearlyEndSharePrice",
                table: "Stocks",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "YearlyStartSharePrice",
                table: "Stocks",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YearlyEndSharePrice",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "YearlyStartSharePrice",
                table: "Stocks");
        }
    }
}
