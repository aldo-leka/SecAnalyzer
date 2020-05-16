select Year, Count(*) from [SecAnalyzer].[dbo].[Stocks]
group by Year
order by year asc