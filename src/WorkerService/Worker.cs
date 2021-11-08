using ApplicationCore;
using ApplicationCore.Models;
using Infastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var now = DateTime.Now;
            DateTime startDate = new DateTime(now.Year, now.Month, now.Day, 9, 0, 0);
            TimeSpan t = startDate.Subtract(now);

            t = t.Hours < -9 ? startDate.AddDays(1).Subtract(now) : t.Hours < 0 ? startDate.AddDays(2).Subtract(now) : t;

            await Task.Delay((t.Hours*3600 + t.Minutes*60 + t.Seconds)*1000, stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {        
                _logger.LogInformation("Worker running at: {time}", DateTime.Now);            

                using(IServiceScope scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<FER_Context>();
                    getData(context);
                }
                await Task.Delay(1800000, stoppingToken);
            }
        }

        private static void getData(FER_Context ctx)
        {
            //string key = ConfigurationManager.AppSettings.Get("API-KEY");
            string key = "GD8LDEQ979MX190X";

            foreach (string from in Enum.GetNames(typeof(Currencies))) {

                foreach (string to in Enum.GetNames(typeof(Currencies))) {
                    if (from.Equals(to))
                        continue;

                    string QUERY_URL = "https://www.alphavantage.co/query?function=CURRENCY_EXCHANGE_RATE&from_currency=" + from + "&to_currency=" + to + "&apikey=" + key;
                    Uri queryUri = new Uri(QUERY_URL);

                    var json_data = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(new WebClient().DownloadString(queryUri));

                    foreach (JObject element in json_data.Values) {

                        var fromCurrency = element.GetValue("1. From_Currency Code").ToString();
                        var toCurrency = element.GetValue("3. To_Currency Code").ToString();

                        ExchangeRateModel entity = ctx.ExchangeRates.Where(item => item.FromCurrency.CurrencyName == fromCurrency).FirstOrDefault(item => item.ToCurrency.CurrencyName == toCurrency);
                        
                        if (entity != null) {

                            History history = new History() {
                                FromCurrencyCode = ctx.Currency.FirstOrDefault(i => i.CurrencyName == fromCurrency),
                                ToCurrencyCode = ctx.Currency.FirstOrDefault(i => i.CurrencyName == toCurrency),
                                ExchangeRate = entity.ExchangeRate,
                                Date = entity.Date,
                                ExchangeRateModel = entity
                            };

                            ctx.History.Add(history);  

                            entity.ExchangeRate = element.GetValue("5. Exchange Rate").ToString();
                            entity.Date = ((DateTime)element.GetValue("6. Last Refreshed"));

                            ctx.ExchangeRates.Update(entity);
                            ctx.SaveChanges();
                        }
                        else {
                            ExchangeRateModel rate = new ExchangeRateModel();

                            rate.FromCurrency = ctx.Currency.FirstOrDefault(i => i.CurrencyName == fromCurrency); 
                            rate.ToCurrency = ctx.Currency.FirstOrDefault(i => i.CurrencyName == toCurrency);
                            rate.ExchangeRate = element.GetValue("5. Exchange Rate").ToString();
                            rate.Date = ((DateTime)element.GetValue("6. Last Refreshed"));

                            ctx.ExchangeRates.Add(rate);
                            ctx.SaveChanges();
                        }
                    }
                }
            }
        }



    }
}

