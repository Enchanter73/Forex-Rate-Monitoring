using ApplicationCore;
using ApplicationCore.Entities;
using Infastructure;
using Infastructure.Repositories;
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
        private readonly IConfiguration _configuration;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            string url = _configuration.GetConnectionString("CurrentData");

            var now = DateTime.Now;
            DateTime startDate = new DateTime(now.Year, now.Month, now.Day, 9, 0, 0);
            TimeSpan t = startDate.Subtract(now);

            t = t.Hours < -9 ? startDate.AddDays(1).Subtract(now) : t.Hours < 0 ? (t.Minutes < -30 ? TimeSpan.FromMinutes(60 + t.Minutes) : TimeSpan.FromMinutes(30+t.Minutes)) : startDate.AddDays(2).Subtract(now);

            await Task.Delay((t.Hours*3600 + t.Minutes*60 + t.Seconds)*1000, stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {        
                _logger.LogInformation("Worker running at: {time}", DateTime.Now);

                using(IServiceScope scope = _serviceProvider.CreateScope())
                {
                    var repo = scope.ServiceProvider.GetRequiredService<Repository>();
                    await getData(url, repo);
                }
                if (now.Hour >= 18)
                    await Task.Delay(54000000, stoppingToken);
                else
                    await Task.Delay(1800000, stoppingToken);
            }
        }

        public static async Task getData(string url, Repository repo)
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client
                .GetAsync(url);

            var responseBody = await response.Content.ReadAsStringAsync();
            JObject result = JObject.Parse(responseBody);

            repo.Add(result);
        }
    }
}

