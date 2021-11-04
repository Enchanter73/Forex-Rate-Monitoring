﻿using ApplicationCore.Models;
using ApplicationCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Infastructure.Data
{
    public class DBOperations
    {
        static FER_Context ctx = new FER_Context();
        public static async Task AddToDB()
        {
            string key = ConfigurationManager.AppSettings.Get("API-KEY");

            foreach(string from in Enum.GetNames(typeof(Currencies)))
            {
                foreach (string to in Enum.GetNames(typeof(Currencies)))
                {
                    if (from.Equals(to))
                        continue;

                    string QUERY_URL = "https://www.alphavantage.co/query?function=CURRENCY_EXCHANGE_RATE&from_currency=" + from + "&to_currency=" + to + "&apikey=" + key;
                    Uri queryUri = new Uri(QUERY_URL);

                    HttpClient client = new HttpClient();
                    var response = await client.GetAsync(queryUri);

                    var jsonString = await response.Content.ReadAsStringAsync();
                    var x = JsonConvert.DeserializeObject<Dictionary<string, object>>(jsonString);

                    //dynamic json_data = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(new WebClient().DownloadString(queryUri));

                    foreach (JsonElement element in x.Values)
                    {
                        var currER = new ExchangeRateModel()
                        {
                            CurrencyCode = element.GetProperty("1. From_Currency Code").ToString() + element.GetProperty("3. To_Currency Code").ToString(),
                            ExchangeRate = element.GetProperty("5. Exchange Rate").ToString(),
                            Date = Convert.ToDateTime(element.GetProperty("6. Last Refreshed").ToString())
                        };

                        ctx.CurrentExchangeRates.Add(currER);
                        
                    }
                }
            }
            ctx.SaveChanges();

        }

        public static IEnumerable<ExchangeRateModel> GetFromDB()
        {
            return ctx.CurrentExchangeRates;
        }
    }
}
