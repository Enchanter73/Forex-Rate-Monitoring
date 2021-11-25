using Infastructure.Repositories;
using Log;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using WorkerService.Helpers;

namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;

        public Worker(IServiceProvider serviceProvider, IConfiguration configuration)
        {
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

            LogHelper.Log(new LogModel()
            {
                EventType = Enums.LogType.Info,
                Message = "Worker.ExecuteAsync",
                MessageDetail = "Worker waiting to start looping",
                CreationDate = DateTime.Now
            });
            await Task.Delay((t.Hours*3600 + t.Minutes*60 + t.Seconds)*1000, stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                LogHelper.Log(new LogModel()
                {
                    EventType = Enums.LogType.Info,
                    Message = "Worker.ExecuteAsync",
                    MessageDetail = "Worker Service started looping",
                    CreationDate = DateTime.Now
                });

                using (IServiceScope scope = _serviceProvider.CreateScope())
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
            try
            {
                HttpClient client = new HttpClient();
                HttpResponseMessage response = await client
                    .GetAsync(url);

                var responseBody = await response.Content.ReadAsStringAsync();
                JObject result = JObject.Parse(responseBody);

                repo.Add(result);
            } 
            catch(Exception ex)
            {
                LogHelper.Log(new LogModel()
                {
                    EventType = Enums.LogType.Error,
                    Message = "Worker.getData",
                    MessageDetail = "An Error occurred while trying to get data from external API",
                    CreationDate = DateTime.Now,
                    Exception = ex
                });
            }

        }
    }
}

