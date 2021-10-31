using Forex_Rate_Monitoring.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using Domain;

namespace Forex_Rate_Monitoring.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        CurrentExchangeRate cer = new CurrentExchangeRate();
        RatesDbContext ratesDbContext = new RatesDbContext();

        public HomeController(ILogger<HomeController> logger) {
            _logger = logger;
        }

        public IActionResult Index() {          
            cer.Currencies = "asdas";
            cer.ExchangeRate = 3.4;
            cer.Date = new DateTime();

            ratesDbContext.currentExchangeRates.Add(cer);
            ratesDbContext.SaveChanges();

            return View();
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
