using Blog.Core.Api.Services.Requirements.PermissionPolicy;
using Blog.Core.StartupModules;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Blog.Core.Api.StartupModels
{
    public class AuthorizationStartupModule : IStartupModule
    {
        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            var permissions = new List<Permission>();
            var permissionRequirement = new PermissionRequirement(
                deniedAction: "api/Account/Denied",
                permissions: permissions
                );

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Permission", policy => policy.Requirements.Add(permissionRequirement));
            });

            services.AddSingleton(permissionRequirement);
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {

        }
    }
}
