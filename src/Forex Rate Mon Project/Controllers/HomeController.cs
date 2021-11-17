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
using ApplicationCore.Entities;
using Infastructure;
using Infastructure.Data;
using Infastructure.Repositories;
using ApplicationCore.ViewModels;
using Domain.ViewModels;

namespace Forex_Rate_Monitoring.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private readonly FER_Context _ctx;
        private Repository _repository;

        public HomeController(ILogger<HomeController> logger, FER_Context ctx, Repository repository) {
            _logger = logger;
            _ctx = ctx;
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Index() {
            return View(_repository.GetCurrentExchangeRatesFromDB());
        }

        [HttpGet]
        public IActionResult History(int from, int to) {
            return View(_repository.GetExchangeRatesFromDB(from, to));
        }

        [HttpGet]
        public IActionResult Sort(int key)
        {      
            return View("Index", _repository.GetSortedExchangeRatesFromDB(key));
        }

        [HttpPost]
        public IActionResult Search(string exchangerate, string basecurrency, string quotecurrency)
        {
            return RedirectToAction("FilteredTable", "Home", new { exchangerate = exchangerate, basecurrency = basecurrency, quotecurrency = quotecurrency});
        }

        [HttpGet]
        public IActionResult FilteredTable(string exchangerate, string basecurrency, string quotecurrency)
        {
            return View("Index", _repository.GetSearchedResult(exchangerate, basecurrency, quotecurrency));
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
