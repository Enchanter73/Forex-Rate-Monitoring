using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Infastructure.Data {
    public class DBReader {
        public static IList<Currency> GetCurrenciesFromDB(FER_Context ctx)
        {
            List<Currency> currencies = ctx.Currency.ToList();
            return currencies;
        }

        public static IList<ExchangeRateModel> GetExchangeRatesFromDB(FER_Context ctx) 
        {
            List<ExchangeRateModel> exchangeRates = ctx.ExchangeRates.ToList();
            return exchangeRates;
        }
    }
}
