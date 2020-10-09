using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Blog.Core.StartupModules
{
    public class ConfigureServicesContext
    {
        public ConfigureServicesContext(IConfiguration configuration, IWebHostEnvironment hostEnvironment, StartupModulesOptions options)
        {
            Configuration = configuration;
            HostingEnvironment = hostEnvironment;
            Options = options;
        }
        public IConfiguration Configuration { get; }

        public IWebHostEnvironment HostingEnvironment { get; }

        public StartupModulesOptions Options { get; }
    }
}
