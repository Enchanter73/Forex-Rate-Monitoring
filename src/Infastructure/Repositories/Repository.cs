using ApplicationCore.Entities;
using ApplicationCore.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infastructure.Repositories
{
    public class Repository
    {
        protected readonly FER_Context _ctx;
        private readonly IDistributedCache _cache;

        public Repository(FER_Context ctx, IDistributedCache cache)
        {
            _ctx = ctx;
            _cache = cache;
        }
        public IList<Currency> GetCurrenciesFromDB()
        {           
            List<Currency> currencies = _ctx.Currency.ToList();
            return currencies;
        }

        public ExchangeRateViewModel GetExchangeRatesFromDB(int from, int to)
        {
            List<Currency> currencies = _ctx.Currency.ToList();           
            IEnumerable<ExchangeRateModel> ratesHistory = _ctx.ExchangeRates.ToList().Where(a => a.FromCurrency.CurrencyId == from && a.ToCurrency.CurrencyId == to);
            return new ExchangeRateViewModel()
            {
                ExchangeRateModels = ratesHistory
            };
        }

        public ExchangeRateViewModel GetCurrentExchangeRatesFromDB()
        {
            IList<Currency> currencies = _ctx.Currency.ToList();
            List<ExchangeRateModel> exchangeRates;           
            IList<ExchangeRateModel> result = new List<ExchangeRateModel>();

            if (_cache.Get("LiveExchangeRates") == null)
            {
                exchangeRates = _ctx.ExchangeRates.ToList();
                var currentRates = exchangeRates.GroupBy(x => new { x.FromCurrency, x.ToCurrency });

                
                foreach (var x in currentRates)
                {
                    result.Add(exchangeRates.Last(e => e.FromCurrency == x.Key.FromCurrency && e.ToCurrency == x.Key.ToCurrency));
                }
                //böyle çalışmıyo string
                _cache.SetString("LiveExchangeRates", result.ToString());
            }
            else
            {
                var rates = _cache.GetString("LiveExchangeRates");
                result = JsonConvert.DeserializeObject<IList<ExchangeRateModel>>(rates);
            }           

            return new ExchangeRateViewModel()
            {
                ExchangeRateModels = result
            };
        }

        public ExchangeRateViewModel GetSortedExchangeRatesFromDB(int key)
        {
            ExchangeRateViewModel model = GetCurrentExchangeRatesFromDB();
            IEnumerable<ExchangeRateModel> result = new List<ExchangeRateModel>();

            switch (key)
            {
                case 1:
                    result = model.ExchangeRateModels.OrderByDescending(x => x.FromCurrency.CurrencyName);
                    break;
                case 2:
                    result = model.ExchangeRateModels.OrderBy(x => x.FromCurrency.CurrencyName);
                    break;
                case 3:
                    result = model.ExchangeRateModels.OrderByDescending(x => x.ToCurrency.CurrencyName);
                    break;
                case 4:
                    result = model.ExchangeRateModels.OrderBy(x => x.ToCurrency.CurrencyName);
                    break;
                case 5:
                    result = model.ExchangeRateModels.OrderByDescending(x => x.ExchangeRate);
                    break;
                case 6:
                    result = model.ExchangeRateModels.OrderBy(x => x.ExchangeRate);
                    break;
            }
            return new ExchangeRateViewModel()
            {
                ExchangeRateModels = result
            };
        }

        public ExchangeRateViewModel GetSearchedResult(string exchangeRate, string baseCurrency, string quoteCurrency)
        {
            ExchangeRateViewModel model = GetCurrentExchangeRatesFromDB();
            IEnumerable<ExchangeRateModel> result = new List<ExchangeRateModel>();

            result = model.ExchangeRateModels.Where(x => double.Parse(x.ExchangeRate, System.Globalization.CultureInfo.GetCultureInfo("en-US")) > double.Parse(exchangeRate)).ToList();

            if(baseCurrency != "Base Currency")
            {
                result = result.Where(x => x.FromCurrency.CurrencyId.ToString() == baseCurrency).ToList();
            }               
            if(quoteCurrency != "Quote Currency")
            {
                result = result.Where(x => x.ToCurrency.CurrencyId.ToString() == quoteCurrency).ToList();
            }                

            return new ExchangeRateViewModel()
            {
                ExchangeRateModels = result
            };
        }

        public void Add(JObject result)
        {
            IList<JToken> results = result["quotes"].Children().ToList();

            foreach (var token in results)
            {
                token.Children().Last().AddAfterSelf(new JProperty("Date", result["requested_time"]));

                ExchangeRateModel exchangeRateModel = JsonConvert.DeserializeObject<ExchangeRateModel>(token.ToString());
                exchangeRateModel.FromCurrency = _ctx.Currency.FirstOrDefault(x => x.CurrencyName == exchangeRateModel.BaseCurreny);
                exchangeRateModel.ToCurrency = _ctx.Currency.FirstOrDefault(x => x.CurrencyName == exchangeRateModel.QuoteCurrency);

                _ctx.ExchangeRates.Add(exchangeRateModel);
            }
            _ctx.SaveChanges();
        }
    }
}
