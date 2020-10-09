using Blog.Core.StartupModules.Internal;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace Blog.Core.StartupModules
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder UseStartupModules(this IWebHostBuilder builder) =>
            UseStartupModules(builder, options => options.DiscoverStartupModules());

        public static IWebHostBuilder UseStartupModules(this IWebHostBuilder builder, params Assembly[] assemblies) =>
            UseStartupModules(builder, options => options.DiscoverStartupModules(assemblies));

        public static IWebHostBuilder UseStartupModules(this IWebHostBuilder builder, Action<StartupModulesOptions> configure)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            var options = new StartupModulesOptions();
            configure(options);

            if (options.StartupModules.Count == 0 && options.ApplicationInitializers.Count == 0)
            {
                return builder;
            }

            var runner = new StartupModuleRunner(options);
            builder.ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<IStartupFilter>(sp => ActivatorUtilities.CreateInstance<ModulesStartupFilter>(sp, runner));

                runner.ConfigureServices(services, hostContext.Configuration, hostContext.HostingEnvironment);
            });

            return builder;
        }
    }
}
