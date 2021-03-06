using Infastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using Infastructure.Data;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Infastructure.Repositories;
using StackExchange.Redis;
using WorkerService.Helpers;
using Log;
using Cache;

namespace WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            CreateDbIfNotExists(host);

            host.Run();
        }

        private static void CreateDbIfNotExists(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<FER_Context>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    LogHelper.Log(new LogModel()
                    {
                        EventType = Enums.LogType.Error,
                        Message = "Program.CreateDbIfNotExists",
                        MessageDetail = "An error occurred creating the DB.",
                        CreationDate = DateTime.Now,
                        Exception = ex
                    });
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)               
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<Worker>();
                    services.AddDbContext<FER_Context>(options =>
                        options.UseSqlServer(hostContext.Configuration.GetConnectionString("ForexDB")));
                    services.AddScoped<Repository>();
                    services.AddStackExchangeRedisCache(options =>
                    {
                        string server = hostContext.Configuration["redis-server"];
                        string port = hostContext.Configuration["redis-port"];
                        string cnstring = $"{server}:{port}";
                        options.Configuration = cnstring;
                    });
                    services.AddSingleton<CacheManager>();
                });
    }
}
