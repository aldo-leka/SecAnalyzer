using SecAnalyzer.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecAnalyzer.Interfaces
{
    public interface IFmpCloudClient
    {
        Task<IEnumerable<BatchRequestResult>> GetAllStocks();
        Task<IEnumerable<YearlyEnterpriseValue>> GetYearlyEnterpriseValues(string symbol);
        Task<IEnumerable<YearlyBalanceSheet>> GetYearlyBalanceSheets(string symbol);
        Task<IEnumerable<YearlyIncomeStatement>> GetYearlyIncomeStatements(string symbol);
        Task<IEnumerable<YearlyKeyMetrics>> GetYearlyKeyMetrics(string symbol);
        Task<HistoricalPriceFull> GetDailyPrices(string symbol);
    }
}
