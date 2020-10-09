using Autofac;
using Autofac.Extras.DynamicProxy;
using Blog.Core.Api.AOP;
using Blog.Core.Api.Filters;
using Blog.Core.Common.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Blog.Core.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(GlobalExceptionFilter));
            });
        }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //builder.RegisterType<AdvertisementServices>().As<IAdvertisementServices>();
            //builder.RegisterType<AdvertisementRepository>().As<IAdvertisementRepository>();
            //builder.RegisterGeneric(typeof(BaseServices<>)).As(typeof(IBaseServices<>));
            //builder.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IBaseRepository<>));

            //services.AddTransient(typeof(IBaseServices<>), typeof(BaseServices<>));
            //services.AddTransient(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            //services.AddTransient<IAdvertisementServices, AdvertisementServices>();
            //services.AddTransient<IAdvertisementRepository, AdvertisementRepository>();

            builder.RegisterType<BlogCacheAOP>();
            builder.RegisterType<BlogRedisCacheAOP>();

            var cacheType = new List<Type>();
            var appSettings = new AppSettings();
            Configuration.Bind("AppSettings", appSettings);
            if (appSettings.RedisCaching.Enabled)
            {
                cacheType.Add(typeof(BlogRedisCacheAOP));
            }
            if (appSettings.MemoryCaching.Enabled)
            {
                cacheType.Add(typeof(BlogCacheAOP));
            }
            //var basePath = Microsoft.DotNet.PlatformAbstractions.ApplicationEnvironment.ApplicationBasePath;//获取项目路径
            //var servicesDllFile = Path.Combine(basePath, @"netstandard2.0\Blog.Core.Services.dll");//获取注入项目绝对路径
            //var repositoryDllFile = Path.Combine(basePath, @"netstandard2.0\Blog.Core.Repository.dll");//获取注入项目绝对路径
            var assemblysServices = Assembly.Load("Blog.Core.Services");// Assembly.LoadFile("Blog.Core.Services");//要记得!!!这个注入的是实现类层，不是接口层！不是 IServices
            builder.RegisterAssemblyTypes(assemblysServices)
                .AsImplementedInterfaces()//指定已扫描程序集中的类型注册为提供所有其实现的接口。
                .InstancePerLifetimeScope()
                .EnableInterfaceInterceptors()
                .InterceptedBy(cacheType.ToArray());
            var assemblysRepository = Assembly.Load("Blog.Core.Repository");//Assembly.LoadFile("Blog.Core.Repository");//模式是 Load(解决方案名)
            builder.RegisterAssemblyTypes(assemblysRepository).AsImplementedInterfaces();

        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
