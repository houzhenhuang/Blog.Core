using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.StartupModules
{
    public interface IApplicationInitializer
    {
        /// <summary>
        /// 调用 <see cref="IApplicationInitializer"/> 实例.
        /// </summary>
        /// <returns></returns>
        Task Invoke();
    }
}
