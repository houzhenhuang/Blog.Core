using AutoMapper;
using Blog.Core.StartupModules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Blog.Core.Api.StartupModels
{
    public class AutoMapperStartupModule : IStartupModule
    {
        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            services.AddAutoMapper(Assembly.Load("Blog.Core.Extensions"));
        }
        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {

        }
    }
}
