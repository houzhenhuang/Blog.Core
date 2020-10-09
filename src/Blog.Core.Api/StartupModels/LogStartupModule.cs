using Blog.Core.StartupModules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.SystemConsole.Themes;
using System;

namespace Blog.Core.Api.StartupModels
{
    public class LogStartupModule : IStartupModule
    {
        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            var elasticUri = context.Configuration["ElasticConfiguration:Uri"];

            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}", theme: AnsiConsoleTheme.Literate)
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticUri))
                {
                    AutoRegisterTemplate = true
                }).CreateLogger();
        }
        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {

        }
    }
}
