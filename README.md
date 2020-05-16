# SecAnalyzer
 This project started with the idea to scrape Edgar SEC filings to get raw financial data and run analysis and backtesting on them.
 The most recent version uses a financial API.
 
 If I'm infringing your copyrights, please let me know and I will remove the code.
 
 To run the application, you need to:
 1. Change the connection string in appsettings.json and SecAnalyzerContext.cs to match with your local database server and database. 
 2. Run "Update-Database" in Package Manager Console (and "Add-Migration MySecAnalyzer" beforehand if that doesn't work instantly).
 3. Insert your fmpcloud.io api key (FmpCloudApiKey) and base address (FmpCloudBaseAddress) in the Config table. 
 4. Run the "seedandanalysis/seed" endpoint to seed the database with the data from the API. Note: It's slow.
 5. Run the currently available endpoints "acquirersmultiple" or "expensivemarket"  to analyze the yearly returns and compound annual growth rates of these investing methods.
 
 There is a problem with the data or my analysis since I get negative returns from "expensivemarket" endpoint which tries to mimic the SP500 "market" returns, while the latter actually produced positive returns for the last 2 decades. Therefore I think that the API doesn't return reliable data.
