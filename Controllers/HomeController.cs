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

namespace Forex_Rate_Monitoring.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;

        static string key = ConfigurationManager.AppSettings.Get("API-KEY");
        
        static string QUERY_URL = "https://www.alphavantage.co/query?function=CURRENCY_EXCHANGE_RATE&from_currency=USD&to_currency=JPY&apikey=" + key;
        Uri queryUri = new Uri(QUERY_URL);

        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }

        public IActionResult Index() {

            dynamic json_data = JsonSerializer.Deserialize<Dictionary<string, dynamic>>(new WebClient().DownloadString(queryUri));
            string fromCurrencyCode = "";
            string toCurrencyCode = "";
            string exchangeRate = "";

            foreach (JsonElement element in json_data.Values)
            {
                fromCurrencyCode = element.GetProperty("1. From_Currency Code").ToString();
                toCurrencyCode = element.GetProperty("3. To_Currency Code").ToString();
                exchangeRate = element.GetProperty("5. Exchange Rate").ToString();
            }

            return View("Index", fromCurrencyCode);
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
