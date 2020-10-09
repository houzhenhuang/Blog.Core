using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Blog.Core.StartupModules
{
    public class ConfigureMiddlewareContext
    {
        public ConfigureMiddlewareContext(IConfiguration configuration, IWebHostEnvironment hostingEnvironment, StartupModulesOptions options)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
            Options = options;
        }
        public IConfiguration Configuration { get; }

        public IWebHostEnvironment HostingEnvironment { get; }

        public StartupModulesOptions Options { get; }
    }
}
