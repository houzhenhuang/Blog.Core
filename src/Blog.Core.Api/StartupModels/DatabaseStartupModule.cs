using Blog.Core.Common.Helper;
using Blog.Core.StartupModules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Serilog;
using SqlSugar;

namespace Blog.Core.Api.StartupModels
{
    public class DatabaseStartupModule : IStartupModule
    {
        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            var appSettings = services.BuildServiceProvider().GetService<IOptions<AppSettings>>().Value;

            services.AddScoped<ISqlSugarClient>(_ =>
            {
                var sqlSugarClient = new SqlSugarClient(new ConnectionConfig()
                {
                    ConnectionString = appSettings?.SqlServer?.SqlServerConnection,
                    DbType = DbType.SqlServer,
                    IsAutoCloseConnection = true,
                    IsShardSameThread = false,
                    MoreSettings = new ConnMoreSettings()
                    {
                        IsAutoRemoveDataCache = true
                    },
                    InitKeyType = InitKeyType.Attribute,
                    AopEvents = new AopEvents
                    {
                        OnLogExecuting = (sql, pars) =>
                        {
                            if (true)
                            {
                                Log.Information($"【参数】：{JsonConvert.SerializeObject(pars)},【SQL语句】：{sql}");
                            }
                        }
                    }
                });
                return sqlSugarClient;
            });
        }
        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {

        }

    }
}
