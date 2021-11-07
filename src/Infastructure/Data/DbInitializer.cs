using ApplicationCore.Models;
using ApplicationCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Infastructure.Data
{
    public class DbInitializer
    {
        public static void Initialize(FER_Context ctx)
        {
            ctx.Database.EnsureCreated();

            if (!ctx.Currency.Any())
            {
                var currencies = new Currency[]
                {
                    new Currency { CurrencyName = "TRY" },
                    new Currency { CurrencyName = "USD" },
                    new Currency { CurrencyName = "EUR" },
                    new Currency { CurrencyName = "GBP" },
                    new Currency { CurrencyName = "JPY" },
                    new Currency { CurrencyName = "CHF" },
                    new Currency { CurrencyName = "KWD" },
                    new Currency { CurrencyName = "RUB" },
                };

                foreach (Currency c in currencies)
                    ctx.Currency.Add(c);

                ctx.SaveChanges();
            }
 
        }    
    }
}
