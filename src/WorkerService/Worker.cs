using ApplicationCore;
using ApplicationCore.Models;
using Infastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            /*var now = DateTime.Now;
            DateTime startDate = new DateTime(now.Year, now.Month, now.Day, 9, 0, 0);
            TimeSpan t = startDate.Subtract(now);

            t = t.Hours < -9 ? startDate.AddDays(1).Subtract(now) : t.Hours < 0 ? startDate.AddDays(2).Subtract(now) : t;

            await Task.Delay((t.Hours*3600 + t.Minutes*60 + t.Seconds)*1000, stoppingToken);*/

            while (!stoppingToken.IsCancellationRequested)
            {        
                _logger.LogInformation("Worker running at: {time}", DateTime.Now);

                using(IServiceScope scope = _serviceProvider.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<FER_Context>();
                    await getData(context);
                }
                await Task.Delay(1800000, stoppingToken);
            }
        }

        protected static async Task getData(FER_Context ctx)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client
                .GetAsync("https://marketdata.tradermade.com/api/v1/live?currency=TRYUSD,TRYEUR,TRYGBP,TRYJPY,TRYCHF,TRYKWD,TRYRUB,USDTRY,USDEUR,USDGBP,USDJPY,USDCHF,USDKWD,USDRUB,EURTRY,EURUSD,EURGBP,EURJPY,EURCHF,EURKWD,EURRUB,GBPTRY,GBPUSD,GBPEUR,GBPJPY,GBPCHF,GBPKWD,GBPRUB,JPYTRY,JPYUSD,JPYEUR,JPYGBP,JPYCHF,JPYKWD,JPYRUB,CHFTRY,CHFUSD,CHFEUR,CHFGBP,CHFJPY,CHFKWD,CHFRUB,KWDTRY,KWDUSD,KWDEUR,KWDGBP,KWDJPY,KWDCHF,JPYRUB,RUBTRY,RUBUSD,RUBEUR,RUBGBP,RUBJPY,RUBCHF,RUBKWD&api_key=Y486dfU4yLB1H7Vvprqm");

            var responseBody = await response.Content.ReadAsStringAsync();
            JObject result = JObject.Parse(responseBody);     
            IList<JToken> results = result["quotes"].Children().ToList();

            foreach (var token in results) {
                token.Children().Last().AddAfterSelf(new JProperty("Date", result["requested_time"]));

                ExchangeRateModel exchangeRateModel = JsonConvert.DeserializeObject<ExchangeRateModel>(token.ToString());
                exchangeRateModel.FromCurrency = ctx.Currency.FirstOrDefault(x => x.CurrencyName == exchangeRateModel.BaseCurreny);
                exchangeRateModel.ToCurrency = ctx.Currency.FirstOrDefault(x => x.CurrencyName == exchangeRateModel.QuoteCurrency);
                            
                ctx.ExchangeRates.Add(exchangeRateModel);                          
            }
            ctx.SaveChanges();
        }
    }
}

