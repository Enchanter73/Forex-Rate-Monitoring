using ApplicationCore;
using ApplicationCore.Models;
using Infastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
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

            t = t.Hours < 0 ? startDate.AddDays(1).Subtract(now) : t.Hours < -9 ? startDate.AddDays(2).Subtract(now) : t;

            await Task.Delay((t.Hours*3600 + t.Minutes*60 + t.Seconds)*1000, stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {        
                _logger.LogInformation("Worker running at: {time}", DateTime.Now);            

                using(IServiceScope scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<FER_Context>();
                    //getData(context);
                }

                await Task.Delay(1800000, stoppingToken);
            }
        }

        private static void getData(FER_Context ctx)
        {
            string key = ConfigurationManager.AppSettings.Get("API-KEY");

            foreach (string from in Enum.GetNames(typeof(Currencies)))
            {
                foreach (string to in Enum.GetNames(typeof(Currencies)))
                {
                    if (from.Equals(to))
                        continue;

                    string QUERY_URL = "https://www.alphavantage.co/query?function=CURRENCY_EXCHANGE_RATE&from_currency=" + from + "&to_currency=" + to + "&apikey=" + key;
                    Uri queryUri = new Uri(QUERY_URL);

                    var json_data = JsonConvert.DeserializeObject<Dictionary<string, ExchangeRateModel>>(new WebClient().DownloadString(queryUri).ToString());

                    foreach (var element in json_data.Values)
                    {
                        ctx.ExchangeRates.Add(element);
                    }

                    ctx.SaveChanges();
                }
            }
        }

    }
}
