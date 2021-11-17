# Forex-Rate-Monitoring
.NET Core 3.1 MVC web application

This web application allows users to see exchange rates between different currencies, with different features. These different features include:

- Sorting the data in UI with different parameters:
    There are six buttons in the UI for sorting table by a column -two for every column (ascending and descending)-
    
- Searching for specific currencies or exchange rates with a search button

- History datas for every exchange rates
    By clicking a currency pair users can see older exchange rates between those currencies


This web application was built with .NET Core 3.1 , by following "Clean Architecture" principles. As a result, There are 4 different layers.

### Application/Domain Layer:
This layer is where all the entities for database and all Viewmodels are defined. This layer is also the most bottom layer which means it is not dependent on any other layer.
    
### Infastructure Layer:
This layer is where repository is defined. All operations related to database is done in this layer.
    
### UI Layer(Forex Rate Monitoring):
This layer is an mvc project and also where the application starts with the Worker. Everything the user see is in view part of this layer.
    
### Worker Layer:
This layer is the worker service of this application. Worker always runs and fetches data from external api service at certain intervals.

Final Dependencies are:


![image](https://user-images.githubusercontent.com/87266516/142194600-2143d697-8ee1-4db8-8814-870e16e475b2.png)

How this application works:
    In this application there are 8 different Currencies. [TRY, USD, EUR, GBP, JPY, CHF KWD, RUB]
    This application has a worker service that continuously running and gettin current exchange rates every 30 min, between 9 A.M - 6 P.M. every day.
    
It has one database with 2 different tables:

Currency Table with [CurrenyId][CurrencyName]

Exchange Rates Table with [ExchangeId][CurrencyId][CurrencyId][ExchangeRate][Date]
        
![image](https://user-images.githubusercontent.com/87266516/142195883-560a9c22-ecf5-4eff-848b-26b5050dcc09.png)

### How to Install:
You can clone project to your local device.

- This project uses Visual Studio's local SQL Server. If you have MSSQL Server you can get Database Connection String and use it instead. (Connection String variable is in the "appsetting.json" file inside both Infastructure and Forex Rate Monitoring projects.)

- Inside the WorkerService/Worker.cs it gets the data from an external api using the url it gets from again "appsettings.json" file. You can add your own key by gettin the key from this link: https://tradermade.com/market-data

- FYI: This project only uses data that exist in databse when printing it to screen. Therefore, you need to use worker to get data from api service before running the web application part.
