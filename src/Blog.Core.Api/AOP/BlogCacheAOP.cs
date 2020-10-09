using Blog.Core.Common.Attributes;
using Blog.Core.Common.MemoryCache;
using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Blog.Core.Api.AOP
{
    /// <summary>
    /// 日志拦截器
    /// </summary>
    public class BlogCacheAOP : CacheAOPBase, IInterceptor
    {
        private readonly ICaching _cache;
        public BlogCacheAOP(ICaching cache)
        {
            _cache = cache;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="invocation">包含被拦截方法的信息</param>
        public void Intercept(IInvocation invocation)
        {
            var method = invocation.MethodInvocationTarget ?? invocation.Method;
            //对当前方法的特性验证
            var cachingAttribute = method.GetCustomAttributes(true).FirstOrDefault(x => x.GetType() == typeof(CachingAttribute)) as CachingAttribute;
            //只有那些指定的才可以被缓存，需要验证
            if (cachingAttribute != null)
            {
                //获取自定义缓存键
                var cacheKey = CustomCacheKey(invocation);
                //根据key获取相应的缓存值
                var cacheValue = _cache.Get(cacheKey);
                if (cacheValue != null)
                {
                    //将当前获取到的缓存值，赋值给当前执行方法
                    invocation.ReturnValue = cacheValue;
                    return;
                }
                //去执行当前的方法
                invocation.Proceed();
                //存入缓存
                if (!string.IsNullOrWhiteSpace(cacheKey))
                {
                    _cache.Set(cacheKey, invocation.ReturnValue);
                }
            }
            else
            {
                invocation.Proceed();//直接执行被拦截方法
            }
        }
        
    }
}
