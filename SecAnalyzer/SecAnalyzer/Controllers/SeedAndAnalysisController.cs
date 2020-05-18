using JeffFerguson.Gepsio;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SecAnalyzer.DTOs;
using SecAnalyzer.Interfaces;
using SecAnalyzer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecAnalyzer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedAndAnalysisController : ControllerBase
    {
        private readonly IFmpCloudClient _fmpCloudClient;
        private readonly SecAnalyzerContext _context;

        public SeedAndAnalysisController(IFmpCloudClient fmpCloudClient, SecAnalyzerContext context)
        {
            _fmpCloudClient = fmpCloudClient;
            _context = context;
        }

        [HttpGet]
        public string Get()
        {
            var magicFormulaUrl = Url.Action("magicformula");
            var acquirersMultipleUrl = Url.Action("acquirersmultiple");
            var seedUrl = Url.Action("seed");
            return $"Try getting\n{magicFormulaUrl} or\n{acquirersMultipleUrl} or\n{seedUrl} to seed the database...";
        }

        [HttpGet("magicformula")]
        public async Task<string> GetMagicFormula()
        {
            //1. Magic Formula
            //1.1 Roic desc
            //roic = net income / tangible assets
            //tangible assets = propertyPlantEquipmentNet + totalCurrentAssets
            //1.2 Earnings yield desc
            //ey = net income / market cap or net income / (market cap + debt - cash)
            //https://fmpcloud.io/api/v3/key-metrics/AAPL?apikey=apikey returnOnTangibleAssets + earningsYield

            await Task.Delay(0);
            return "Magic Formula";
        }

        /// <summary>
        /// Get top 30 cheapest stocks based on enterprise value / operating earnings aka VC acquirer's multiple.
        /// </summary>
        [HttpGet("acquirersmultiple")]
        public string GetAcquirersMultiple(decimal initialAmount = 10000)
        {
            var amount = initialAmount;

            //2. Acquirer's multiple
            //am = (m.cap + debt - cash) / (revenue - cost of goods sold - selling, general, adm costs - depreciation & amortization)
            var groupings = _context.Stocks
                .Where(stock => stock.Year > 1 && stock.Year < 2020)
                .OrderBy(stock => stock.Year)
                .AsEnumerable()
                .GroupBy(stock => stock.Year);

            var result = $"Compounding for {groupings.Count()} years from ";
            result += groupings.First().Key + " to " + groupings.Last().Key + ".\n\n";

            var years = 0;

            //TODO Sell only if worth selling.
            foreach (var grouping in groupings)
            {
                var topYearlyStocks = new List<Stock>();
                var year = grouping.Key;
                var stocks = grouping.Where(stock =>
                    stock.MarketCap > 0 &&
                    stock.Revenue - stock.CostOfRevenue - stock.ResearchAndDevelopmentCosts - stock.SellingGeneralAndAdministrativeCosts - stock.DepreciationAndAmortization != 0
                    && stock.YearlyStartSharePrice > 0 && stock.YearlyEndSharePrice > 0);

                if (stocks.Count() > 0)
                {
                    ++years;

                    topYearlyStocks.AddRange(
                        stocks.Select(stock => new
                        {
                            Stock = stock,
                            //TODO
                            //Add price of specific reporting date (FilingDatePrice) and 1 year after that (FilingDateAfterOneYearPrice).
                            AcquirersMultiple = (decimal)(stock.MarketCap + stock.TotalDebt - stock.Cash)
                                / (stock.Revenue - stock.CostOfRevenue - stock.ResearchAndDevelopmentCosts - stock.SellingGeneralAndAdministrativeCosts - stock.DepreciationAndAmortization)
                        })
                        .OrderBy(entity => entity.AcquirersMultiple)
                        .Take(30)
                        .Select(entity => entity.Stock)
                        .ToList()
                    );

                    //Possibilities
                    //Negative EV, Positive earnings = - GOOD (more cash, positive earnings)
                    //Positive EV, Negative earnings = - BAD (less cash, more debt, negative earnings)
                    //Negative EV, Negative earnings = + GOOD? (more cash, negative earnings)
                    //Positive EV, Positive earnings = + NORMAL?
                    //Basically Big EV = Big Cost = BAD

                    var compositeStartPrice = topYearlyStocks.Sum(stock => stock.YearlyStartSharePrice);
                    var compositeEndPrice = topYearlyStocks.Sum(stock => stock.YearlyEndSharePrice);
                    var change = compositeEndPrice / compositeStartPrice - 1;
                    var yearlyInterest = amount * change;
                    amount += yearlyInterest;

                    result += $"Year {year}\n";
                    result += $"Change: {change * 100:0.##}%\n";
                    result += $"Interest {(long)yearlyInterest}\n";
                    result += $"Amount: {(long)amount}\n";
                    result += "Picks: " + string.Join(',', topYearlyStocks.Select(stock => stock.Symbol)) + "\n\n";
                }
            }

            var cagr = Math.Pow((double)(amount / initialAmount), 1 / (double)years) - 1;

            result += $"Compound annual growth rate: {cagr * 100:0.##}%\n";
            result += $"Total interest: Euro {(long)amount - initialAmount}\n";
            result += $"End amount: Euro {(long)amount}\n";

            return result;
        }

        /// <summary>
        /// Get top 500 most expensive stocks returns.
        /// </summary>
        [HttpGet("expensivemarket")]
        public string GetExpensiveMarket(decimal initialAmount = 10000)
        {
            var amount = initialAmount;
            var groupings = _context.Stocks
                .Where(stock => stock.Year > 2000)
                .OrderBy(stock => stock.Year)
                .AsEnumerable()
                .GroupBy(stock => stock.Year);

            var result = $"Compounding for {groupings.Count()} years from ";
            result += groupings.First().Key + " to " + groupings.Last().Key + ".\n\n";

            var years = 0;
            foreach (var grouping in groupings)
            {
                var topYearlyStocks = new List<Stock>();
                var year = grouping.Key;
                var stocks = grouping.Where(stock => stock.MarketCap > 0 && stock.YearlyStartSharePrice > 0 && stock.YearlyEndSharePrice > 0);

                if (stocks.Count() > 0)
                {
                    ++years;

                    topYearlyStocks.AddRange(stocks
                        .OrderBy(stock => stock.MarketCap)
                        .Take(500)
                        .ToList()
                    );

                    var compositeStartPrice = topYearlyStocks.Sum(stock => stock.YearlyStartSharePrice);
                    var compositeEndPrice = topYearlyStocks.Sum(stock => stock.YearlyEndSharePrice);
                    var change = compositeEndPrice / compositeStartPrice - 1;
                    var yearlyInterest = amount * change;
                    amount += yearlyInterest;

                    result += $"Year {year}\n";
                    result += $"Change: {change * 100:0.##}%\n";
                    result += $"Interest {(long)yearlyInterest}\n";
                    result += $"Amount: {(long)amount}\n\n";
                }
            }

            var cagr = Math.Pow((double)(amount / initialAmount), 1 / (double)years) - 1;

            result += $"Compound annual growth rate: {cagr * 100:0.##}%\n";
            result += $"Total interest: Euro {(long)amount - initialAmount}\n";
            result += $"End amount: Euro {(long)amount}\n";

            return result;
        }

        /// <summary>
        /// Used to patch database (new or empty) columns with new data. Currently for start and end stock prices.
        /// </summary>
        [HttpGet("test")]
        public async Task<string> GetTest()
        {
            var apiStocks = await _fmpCloudClient.GetAllStocks();

            foreach (var apiStock in apiStocks)
            {
                var existing = _context.Stocks.Where(stock => stock.Symbol == apiStock.Symbol);
                if (existing.Count() > 0)
                {
                    var dayPrices = await _fmpCloudClient.GetDailyPrices(apiStock.Symbol);
                    foreach (var stock in existing)
                    {
                        var yearlyDayPrices = dayPrices.DailyPrices.Where(dayPrice => DateTime.ParseExact(dayPrice.Date ?? "0001-01-01", "yyyy-MM-dd", null).Year == stock.Year)
                            .OrderBy(priceComponent => DateTime.ParseExact(priceComponent.Date ?? "0001-01-01", "yyyy-MM-dd", null));
                        stock.YearlyStartSharePrice = yearlyDayPrices.Count() > 0 ? yearlyDayPrices.First().Price : 0m;
                        stock.YearlyEndSharePrice = yearlyDayPrices.Count() > 0 ? yearlyDayPrices.Last().Price : 0m;
                    }

                    _context.SaveChanges();
                }
            }

            return $"Done with {apiStocks.Count()} stocks";
        }

        [HttpGet("seed")]
        public async Task<string> Seed()
        {
            //Clear current log.
            _context.Logs.RemoveRange(_context.Logs);
            //Clear current stocks.
            _context.Stocks.RemoveRange(_context.Stocks);
            _context.SaveChanges();

            var apiStocks = await _fmpCloudClient.GetAllStocks();

            //1. Iterate all stocks.
            foreach (var apiStock in apiStocks)
            {
                var yearlyBalanceSheetsTask = _fmpCloudClient.GetYearlyBalanceSheets(apiStock.Symbol);
                var yearlyIncomeStatementsTask = _fmpCloudClient.GetYearlyIncomeStatements(apiStock.Symbol);
                var yearlyKeyMetricsTask = _fmpCloudClient.GetYearlyKeyMetrics(apiStock.Symbol);
                var dayPricesTask = _fmpCloudClient.GetDailyPrices(apiStock.Symbol);

                var yearlyBalanceSheets = new List<YearlyBalanceSheet>();
                var yearlyIncomeStatements = new List<YearlyIncomeStatement>();
                var yearlyKeyMetrics = new List<YearlyKeyMetrics>();
                var dayPrices = new HistoricalPriceFull
                {
                    DailyPrices = new List<HistoricalPrice>()
                };

                try
                {
                    yearlyBalanceSheets = (await yearlyBalanceSheetsTask).ToList();
                }
                catch (Exception e)
                {
                    _context.Logs.Add(new Log
                    {
                        Symbol = apiStock.Symbol,
                        Api = "BalanceSheet",
                        Error = e.Message
                    });
                    continue;
                }

                try
                {
                    yearlyIncomeStatements = (await yearlyIncomeStatementsTask).ToList();
                }
                catch (Exception e)
                {
                    _context.Logs.Add(new Log
                    {
                        Symbol = apiStock.Symbol,
                        Api = "IncomeStatement",
                        Error = e.Message
                    });
                    continue;
                }

                try
                {
                    yearlyKeyMetrics = (await yearlyKeyMetricsTask).ToList();
                }
                catch (Exception e)
                {
                    _context.Logs.Add(new Log
                    {
                        Symbol = apiStock.Symbol,
                        Api = "KeyMetrics",
                        Error = e.Message
                    });
                    continue;
                }

                try
                {
                    dayPrices = await dayPricesTask;
                }
                catch (Exception e)
                {
                    _context.Logs.Add(new Log
                    {
                        Symbol = apiStock.Symbol,
                        Api = "DayPrices",
                        Error = e.Message
                    });
                    //continue;
                }

                var stocks = new List<Stock>();

                foreach (var yearlyBalanceSheet in yearlyBalanceSheets)
                {
                    var year = DateTime.ParseExact(yearlyBalanceSheet.FilingDate ?? "0001-01-01", "yyyy-MM-dd", null).Year;

                    var stock = stocks.Where(stock => stock.Year == year).FirstOrDefault();
                    if (stock == null)
                    {
                        stock = new Stock
                        {
                            Symbol = apiStock.Symbol,
                            Year = year
                        };
                        stocks.Add(stock);
                    }
                    stock.TotalDebt = yearlyBalanceSheet.TotalLiabilities;
                    stock.Cash = yearlyBalanceSheet.CashAndShortTermInvestments;
                    stock.NetPropertyPlantAndEquipment = yearlyBalanceSheet.NetPropertyPlantAndEquipment;
                    stock.TotalCurrentAssets = yearlyBalanceSheet.TotalCurrentAssets;
                }

                foreach (var yearlyIncomeStatement in yearlyIncomeStatements)
                {
                    var year = DateTime.ParseExact(yearlyIncomeStatement.FilingDate ?? "0001-01-01", "yyyy-MM-dd", null).Year;
                    var stock = stocks.Where(stock => stock.Year == year).FirstOrDefault();
                    if (stock == null)
                    {
                        stock = new Stock
                        {
                            Symbol = apiStock.Symbol,
                            Year = year
                        };
                        stocks.Add(stock);
                    }
                    stock.Revenue = yearlyIncomeStatement.Revenue;
                    stock.CostOfRevenue = yearlyIncomeStatement.CostOfRevenue;
                    stock.SellingGeneralAndAdministrativeCosts = yearlyIncomeStatement.GeneralAndAdministrativeExpenses + yearlyIncomeStatement.SellingAndMarketingExpenses;
                    stock.DepreciationAndAmortization = yearlyIncomeStatement.DepreciationAndAmortization;
                    stock.NetIncome = yearlyIncomeStatement.NetIncome;
                }

                foreach (var yearlyKeyMetric in yearlyKeyMetrics)
                {
                    var year = DateTime.ParseExact(yearlyKeyMetric.Date ?? "0001-01-01", "yyyy-MM-dd", null).Year;
                    var stock = stocks.Where(stock => stock.Year == year).FirstOrDefault();
                    if (stock == null)
                    {
                        stock = new Stock
                        {
                            Symbol = apiStock.Symbol,
                            Year = year
                        };
                        stocks.Add(stock);
                    }
                    stock.MarketCap = yearlyKeyMetric.MarketCap;
                    stock.RevenuePerShare = yearlyKeyMetric.RevenuePerShare;
                    stock.NetIncomePerShare = yearlyKeyMetric.NetIncomePerShare;
                    stock.FreeCashFlowPerShare = yearlyKeyMetric.FreeCashFlowPerShare;
                    stock.TangibleBookValuePerShare = yearlyKeyMetric.TangibleBookValuePerShare;
                    stock.EnterpriseValue = yearlyKeyMetric.EnterpriseValue;
                    stock.PERatio = yearlyKeyMetric.PERatio;
                    stock.PriceToFreeCashFlowRatio = yearlyKeyMetric.PriceToFreeCashFlowRatio;
                    stock.PriteToTangibleBookRatio = yearlyKeyMetric.PriteToTangibleBookRatio;
                    stock.EarningsYield = yearlyKeyMetric.EarningsYield;
                    stock.ReturnOnTangibleAssets = yearlyKeyMetric.ReturnOnTangibleAssets;
                    stock.GrahamNetNet = yearlyKeyMetric.GrahamNetNet;
                }

                foreach (var stock in stocks)
                {
                    var yearlyDayPrices = dayPrices.DailyPrices.Where(dayPrice => DateTime.ParseExact(dayPrice.Date ?? "0001-01-01", "yyyy-MM-dd", null).Year == stock.Year);
                    stock.YearlyLowSharePrice = yearlyDayPrices.Count() > 0 ? yearlyDayPrices.Min(priceComponent => priceComponent.Price) : 0m;
                    stock.YearlyHighSharePrice = yearlyDayPrices.Count() > 0 ? yearlyDayPrices.Max(priceComponent => priceComponent.Price) : 0m;
                }

                _context.Stocks.AddRange(stocks);
                _context.SaveChanges();

                //if (++counter > limit) break;
            }

            //TODO
            //Await all tasks instead of batches like above to optimize the speed.

            return $"Updated db with {apiStocks.Count()} stocks.";
        }

        //1st attempt was to use XBRL parser.
        [HttpGet("xbrl")]
        public string GetXbrl()
        {
            var xbrlDoc = new XbrlDocument();
            xbrlDoc.Load("https://www.sec.gov/Archives/edgar/data/874292/000144526020000011/aey-20191231.xml");
            return JsonConvert.SerializeObject(xbrlDoc.XbrlFragments
                .SelectMany(frag => frag.Facts)
                .Select(fact => new DTOs.FactResult
                {
                    Name = fact.Name,
                    Value = (fact as Item).Value
                }), Formatting.Indented);
        }
    }
}