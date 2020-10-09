using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Blog.Core.Api.AuthHelper;
using Blog.Core.Common.Helper;
using Blog.Core.IRepository.Data;
using Blog.Core.StartupModules;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;

namespace Blog.Core.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var seedData = services.GetService<ISeedData>();

                    if (args.Contains("/seed"))
                    {
                        seedData.SeedAsync().Wait();
                    }
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex.ToString(), "An error occurred seeding the DB.");
                }
            }
            host.Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureServices((hostContext, services) =>
                    {
                        services.Configure<AppSettings>(hostContext.Configuration.GetSection("AppSettings"));

                        services.Configure<JwtSettings>(hostContext.Configuration.GetSection("JwtSettings"));

                    });
                    webBuilder.UseStartupModules(options =>
                    {
                        options.DiscoverStartupModules();

                    }).UseStartup<Startup>();

                    webBuilder.UseSerilog();
                });
    }
}
