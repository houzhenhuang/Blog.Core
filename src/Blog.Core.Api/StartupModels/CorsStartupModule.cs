using Blog.Core.Common.Helper;
using Blog.Core.StartupModules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Blog.Core.Api.StartupModels
{
    public class CorsStartupModule : IStartupModule
    {
        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            var appSettings = services.BuildServiceProvider().GetService<IOptions<AppSettings>>().Value;
            services.AddCors(options =>
            {
                options.AddPolicy("AllRequests", policy =>
                {
                    policy
                    .SetIsOriginAllowed(_ => true)//验证传入的 origin 源，如果验证通过则返回 true
                    .AllowAnyMethod()//允许任何方式
                    .AllowAnyHeader()//允许任何头

                    .AllowCredentials();//允许cookie
                });

                //一般采用这种方法
                options.AddPolicy("LimitRequests", policy =>
                {
                    policy
                    .WithOrigins(appSettings.Cors.Ips.ToArray())//支持多个域名端口，注意端口号后不要带/斜杆：比如localhost:8000/，是错的
                    .AllowAnyHeader()//Ensures that the policy allows any header.
                    .AllowAnyMethod();
                });
            });
        }
        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {
            app.UseCors("LimitRequests");
        }
    }
}
