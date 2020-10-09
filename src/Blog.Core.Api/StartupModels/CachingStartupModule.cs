using Blog.Core.Common.MemoryCache;
using Blog.Core.Common.Redis;
using Blog.Core.StartupModules;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Core.Api.StartupModels
{
    public class CachingStartupModule : IStartupModule
    {
        public void ConfigureServices(IServiceCollection services, ConfigureServicesContext context)
        {
            services.AddSingleton<IMemoryCache>(factory =>
            {
                var cache = new MemoryCache(new MemoryCacheOptions());
                return cache;
            });
            services.AddScoped<ICaching, Common.MemoryCache.MemoryCaching>();
            services.AddSingleton<IRedisCacheManager, RedisCacheManager>();
        }
        public void Configure(IApplicationBuilder app, ConfigureMiddlewareContext context)
        {

        }
    }
}
