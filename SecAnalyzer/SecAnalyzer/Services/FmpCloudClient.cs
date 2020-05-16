using RestSharp;
using SecAnalyzer.DTOs;
using SecAnalyzer.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SecAnalyzer.Services
{
    public class FmpCloudClient : IFmpCloudClient
    {
        private readonly IConfig _config;

        public FmpCloudClient(IConfig config)
        {
            _config = config;
        }

        /// <summary>
        /// Example: https://fmpcloud.io/api/v3/batch-request-end-of-day-prices?apikey=apikey
        /// </summary>
        public async Task<IEnumerable<BatchRequestResult>> GetAllStocks()
        {
            var resource = "batch-request-end-of-day-prices";
            var request = new RestRequest(resource);
            return await ExecuteAsync<List<BatchRequestResult>>(request);
        }

        /// <summary>
        /// Example: https://fmpcloud.io/api/v3/enterprise-values/AAPL?apikey=apikey
        /// </summary>
        public async Task<IEnumerable<YearlyEnterpriseValue>> GetYearlyEnterpriseValues(string symbol)
        {
            var resource = $"enterprise-values/{symbol}";
            var request = new RestRequest(resource);
            return await ExecuteAsync<List<YearlyEnterpriseValue>>(request);
        }

        /// <summary>
        /// Example: https://fmpcloud.io/api/v3/balance-sheet-statement/AAPL?apikey=apikey
        /// </summary>
        public async Task<IEnumerable<YearlyBalanceSheet>> GetYearlyBalanceSheets(string symbol)
        {
            var resource = $"balance-sheet-statement/{symbol}";
            var request = new RestRequest(resource);
            return await ExecuteAsync<List<YearlyBalanceSheet>>(request);
        }

        /// <summary>
        /// Example: https://fmpcloud.io/api/v3/income-statement/AAPL?apikey=apikey
        /// </summary>
        public async Task<IEnumerable<YearlyIncomeStatement>> GetYearlyIncomeStatements(string symbol)
        {
            var resource = $"income-statement/{symbol}";
            var request = new RestRequest(resource);
            return await ExecuteAsync<List<YearlyIncomeStatement>>(request);
        }

        /// <summary>
        /// https://fmpcloud.io/api/v3/key-metrics/AAPL?apikey=apikey
        /// </summary>
        public async Task<IEnumerable<YearlyKeyMetrics>> GetYearlyKeyMetrics(string symbol)
        {
            var resource = $"key-metrics/{symbol}";
            var request = new RestRequest(resource);
            return await ExecuteAsync<List<YearlyKeyMetrics>>(request);
        }

        /// <summary>
        /// https://fmpcloud.io/api/v3/historical-price-full/AAPL?serietype=line&apikey=apikey
        /// </summary>
        public async Task<HistoricalPriceFull> GetDailyPrices(string symbol)
        {
            var action = $"historical-price-full/{symbol}?serietype=line";
            var request = new RestRequest(action);
            return await ExecuteAsync<HistoricalPriceFull>(request);
        }

        private async Task<T> ExecuteAsync<T>(IRestRequest request) where T : new()
        {
            request.AddParameter("apikey", _config.FmpCloudApiKey);

            var client = new RestClient(_config.FmpCloudBaseAddress);
            var responseTask = client.ExecuteAsync<T>(request);
            var response = await responseTask;

            return HandleResponse(response, r => r.Data);
        }

        private TResult HandleResponse<T, TResult>(IRestResponse<T> response, Func<IRestResponse<T>, TResult> resultSelector)
        {
            var request = response.Request;
            try
            {
                if (response.ResponseStatus == ResponseStatus.Completed)
                {
                    var code = (int)response.StatusCode;
                    if (code < 200 || code > 299)
                    {
                        throw new Exception("Fmp Cloud Response Error: " + response.StatusCode + " - " + response.Content);
                    }
                    else
                    {
                        var result = resultSelector.Invoke(response);
                        //LogRequest(request, response?.Content, response?.StatusCode, null);
                        return result;
                    }
                }
                else
                {
                    throw new Exception("API Error: " + response.ErrorMessage, response.ErrorException);
                }
            }
            catch (Exception e)
            {
                //LogRequest(request, response?.Content, response?.StatusCode, e);
                throw;
            }
        }
    }
}
