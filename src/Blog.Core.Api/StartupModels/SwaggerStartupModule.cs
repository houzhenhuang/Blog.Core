using Blog.Core.StartupModules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.IO;
using System.Linq;
using static Blog.Core.Api.SwaggerHelper.CustomApiVersion;

namespace Blog.Core.Api.StartupModels
{
    public class SwaggerStartupModule : IStartupModule
    {
        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            services.AddSwaggerGen(options =>
            {
                var apiName = "Blog.Core";
                typeof(ApiVersions).GetEnumNames().ToList().ForEach(version =>
                {
                    options.SwaggerDoc(version, new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Version = version,
                        Title = $"{apiName} 接口文档",
                        Description = $"{apiName} HTTP API " + version,
                        //TermsOfService = new Uri("https://www.cnblogs.com/hhzblogs"),
                        Contact = new OpenApiContact
                        {
                            Name = apiName,
                            Email = "johnny@163.com",
                            Url = new Uri("https://www.cnblogs.com/hhzblogs")
                        },
                        License = new OpenApiLicense { Name = apiName + " 官方文档", Url = new Uri("https://www.cnblogs.com/hhzblogs") }
                    });
                });

                var basePath = AppContext.BaseDirectory;

                var xmlPath = Path.Combine(basePath, "Blog.Core.Api.xml");
                options.IncludeXmlComments(xmlPath, true);

                var modelXmlPath = Path.Combine(basePath, "Blog.Core.Model.xml");
                options.IncludeXmlComments(modelXmlPath);

                //开启加权小锁
                options.OperationFilter<AddResponseHeadersFilter>();
                options.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();
                //在header中添加token,传递到后台
                options.OperationFilter<SecurityRequirementsOperationFilter>();

                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "在下框中输入请求头中需要添加Jwt授权Token：Bearer Token",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
            });
        }
        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                //options.SwaggerEndpoint("/swagger/v1/swagger.json", "ApiHelper V1");
                //options.RoutePrefix = "";
                typeof(ApiVersions).GetEnumNames().OrderByDescending(v => v).ToList().ForEach(version =>
                {
                    options.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"Blog.Core {version}");
                });
                options.RoutePrefix = "";
            });
        }

    }
}
