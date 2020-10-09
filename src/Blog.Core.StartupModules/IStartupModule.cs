using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Blog.Core.StartupModules
{
    /// <summary>
    /// 定义一个启动模块
    /// 配置应用程序和中间件
    /// </summary>
    public interface IStartupModule
    {
        /// <summary>
        /// 配置应用程序服务
        /// </summary>
        /// <param name="services"><see cref=""/></param>
        /// <param name="context"></param>
        void ConfigureServices(IServiceCollection services, ConfigureServicesContext context);
        /// <summary>
        /// 配置应用程序中间件管道
        /// </summary>
        /// <param name="app"></param>
        /// <param name="context"></param>
        void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context);
    }
}
