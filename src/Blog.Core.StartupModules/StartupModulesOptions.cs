using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Blog.Core.StartupModules
{
    public class StartupModulesOptions
    {
        public ICollection<IStartupModule> StartupModules { get; } = new List<IStartupModule>();
        public ICollection<Type> ApplicationInitializers { get; } = new List<Type>();

        /// <summary>
        /// GetEntryAssembly获取的是当前应用程序第一个启动的程序，一般就是xxx.exe文件。
        /// GetExecutingAssembly获取的是当前执行的方法所在的程序文件，可能是.exe，也可能是当前方法所在的.dll文件。
        /// </summary>
        public void DiscoverStartupModules() => DiscoverStartupModules(Assembly.GetEntryAssembly()!);
        public void DiscoverStartupModules(params Assembly[] assemblies)
        {
            if (assemblies == null || assemblies.Length == 0 || assemblies.All(a => a == null))
            {
                throw new ArgumentException("没有从指定的程序集发现启动模块");
            }
            //遍历所有程序集中的所有类型
            foreach (var type in assemblies.SelectMany(a => a.ExportedTypes))
            {
                //type类型是否实现了IStartupModule
                if (typeof(IStartupModule).IsAssignableFrom(type))
                {
                    var instance = Activate(type);
                    StartupModules.Add(instance);
                }

                if (typeof(IApplicationInitializer).IsAssignableFrom(type))
                {
                    ApplicationInitializers.Add(type);
                }
            }
        }
        /// <summary>
        /// 添加类型为<typeparamref name="T"/>的<see cref="IStartupModule"/>实例
        /// </summary>
        /// <typeparam name="T"><see cref="IStartupModule"/>类型.</typeparam>
        /// <param name="startupModule"><see cref="IStartupModule"/>类型的实例.</param>
        public void AddStartupModule<T>(T startupModule) where T : IStartupModule
            => StartupModules.Add(startupModule);
        public void AddStartupModule<T>() where T : IStartupModule
            => AddStartupModule(typeof(T));
        public void AddStartupModule(Type type)
        {
            if (typeof(IStartupModule).IsAssignableFrom(type))
            {
                var instace = Activate(type);
                StartupModules.Add(instace);
            }
            else
            {
                throw new ArgumentException(
                    $"指定的启动模块 '{type.Name}' 未实现 {nameof(IStartupModule)}.",
                    nameof(type));
            }
        }
        private IStartupModule Activate(Type type)
        {
            try
            {
                return (IStartupModule)Activator.CreateInstance(type);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"未能为{nameof(IStartupModule)}创建类型为{type.Name}的实例", ex);
            }
        }
    }
}
