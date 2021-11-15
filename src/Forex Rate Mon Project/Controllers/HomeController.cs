using Forex_Rate_Monitoring.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net;
using System.Configuration;
using WorkerService;
using ApplicationCore.Models;
using Infastructure;
using Infastructure.Data;

namespace Forex_Rate_Monitoring.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly FER_Context _ctx;

        public HomeController(ILogger<HomeController> logger, FER_Context ctx) {
            _logger = logger;
            _ctx = ctx;
        }

        public IActionResult Index() {
            IList<Currency> currencies = DBReader.GetCurrenciesFromDB(_ctx);
            IList<ExchangeRateModel> exchangeRates = DBReader.GetExchangeRatesFromDB(_ctx);
            var currentRates = exchangeRates.GroupBy(x => new { x.FromCurrency, x.ToCurrency });

            IList<ExchangeRateModel> result = new List<ExchangeRateModel>();
            foreach(var x in currentRates)
            {
                result.Add(exchangeRates.Last(e => e.FromCurrency == x.Key.FromCurrency && e.ToCurrency == x.Key.ToCurrency));
            }
            return View(result);
        }

        public IActionResult History(int from, int to) {
            IList<Currency> currencies = DBReader.GetCurrenciesFromDB(_ctx);
            IList<ExchangeRateModel> exchangeRates = DBReader.GetExchangeRatesFromDB(_ctx);
            IEnumerable<ExchangeRateModel> ratesHistory = DBReader.GetExchangeRatesFromDB(_ctx).Where(a => a.FromCurrency.CurrencyId == from && a.ToCurrency.CurrencyId == to);
            return View(ratesHistory);
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
