using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.StartupModules
{
    public class StartupModuleRunner
    {
        private readonly StartupModulesOptions _options;
        public StartupModuleRunner(StartupModulesOptions options)
        {
            _options = options;
        }
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            var ctx = new ConfigureServicesContext(configuration, hostEnvironment, _options);
            foreach (var item in _options.StartupModules)
            {
                item.ConfigureServices(services, ctx);
            }
        }
        public void Configure(IApplicationBuilder app, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            var ctx = new ConfigureMiddlewareContext(configuration, hostEnvironment, _options);
            foreach (var item in _options.StartupModules)
            {
                item.Configure(app, ctx);
            }
        }

        public async Task RunApplicationInitializers(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var applicationInitalizers = _options.ApplicationInitializers
                .Select(t =>
                {
                    try
                    {
                        return ActivatorUtilities.CreateInstance(scope.ServiceProvider, t);
                    }
                    catch (Exception ex)
                    {
                        throw new InvalidOperationException($"创建 {nameof(IApplicationInitializer)} 实例 '{t.Name}'.", ex);
                    }
                })
                .Cast<IApplicationInitializer>();

            foreach (var initializer in applicationInitalizers)
            {
                try
                {
                    await initializer.Invoke();
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException($"在执行 {nameof(IApplicationInitializer)} '{initializer.GetType().Name}'期间发生异常.", ex);
                }
            }
        }
    }
}
