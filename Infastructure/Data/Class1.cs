using ApplicationCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Infastructure.Data
{
    public class Class1
    {
        public static void getData()
        {
            string key = ConfigurationManager.AppSettings.Get("API-KEY");

            string QUERY_URL = "https://www.alphavantage.co/query?function=CURRENCY_EXCHANGE_RATE&from_currency=" + "USD" + "&to_currency=" + "JPY" + "&apikey=" + key;
            Uri queryUri = new Uri(QUERY_URL);

            dynamic json_data = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(new WebClient().DownloadString(queryUri));

            foreach (JsonElement element in json_data.Values)
            {
                using (var ctx = new FER_Context())
                {
                    var currER = new ExchangeRateModel()
                    {
                        CurrencyCode = element.GetProperty("1. From_Currency Code").ToString() + element.GetProperty("3. To_Currency Code").ToString(),
                        ExchangeRate = element.GetProperty("5. Exchange Rate").ToString(),
                        Date = Convert.ToDateTime(element.GetProperty("6. Last Refreshed").ToString())
                    };

                    ctx.CurrentExchangeRates.Add(currER);
                    ctx.SaveChanges();
                }
            }
        }
    }
}
