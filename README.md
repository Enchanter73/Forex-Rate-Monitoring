# Forex-Rate-Monitoring
.NET Core 3.1 MVC web application

This web application allows users to see exchange rates between different currencies, with different features. These different features include:

- Sorting the data in UI with different parameters:
    There are six buttons in the UI -2 for every parameter (ascending and descending)-
    
- Searching for specific currencies or exchange rates with a search button

- History datas for every exchange rates
    By clicking a currency pair users can see older exchange rates between those currencies


This web application was built with .NET Core 3.1 , by following "Clean Architecture" principles. As a result, There are 4 different layers.

Application/Domain Layer: 
    This layer is where all the entities for database and all Viewmodels are defined. This layer is also the most bottom layer which means it is not dependent on any other layer.
    
Infastructure Layer:
    This layer is where repository is defined. All operations related to database is done in this layer.
    
UI Layer(Forex Rate Monitoring):
    This layer is an mvc project and also where the application starts with the Worker. Everything the user see is in view part of this layer.
    
Worker Layer:
    This layer is the worker service of this application. Worker always runs and fetches data from external api service at certain intervals.

How this application works:
    In this application there are 8 different Currencies. [TRY, USD, EUR, GBP, JPY, CHF KWD, RUB]
    This application has a worker service that continuously running and gettin current exchange rates every 30 min, between 9 A.M - 6 P.M. every day.
    
    It has one database with 2 different tables:
        Currency Table with [CurrenyId][CurrencyName]
        Exchange Rates Table with [ExchangeId][CurrencyId][CurrencyId][ExchangeRate][Date]
    
    
