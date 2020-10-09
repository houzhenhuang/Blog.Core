using Blog.Core.Common.Attributes;
using Blog.Core.Common.Redis;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Api.AOP
{
    public class BlogRedisCacheAOP : CacheAOPBase, IInterceptor
    {
        private readonly IRedisCacheManager _cache;
        public BlogRedisCacheAOP(IRedisCacheManager cache)
        {
            _cache = cache;
        }

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
                var cacheValue = _cache.GetValue(cacheKey);
                if (cacheValue != null)
                {
                    Type returnType;
                    if (typeof(Task).IsAssignableFrom(method.ReturnType))
                    {
                        returnType = method.ReturnType.GenericTypeArguments.FirstOrDefault();
                    }
                    else
                    {
                        returnType = method.ReturnType;
                    }
                    dynamic result = Newtonsoft.Json.JsonConvert.DeserializeObject(cacheValue, returnType);
                    invocation.ReturnValue = (typeof(Task).IsAssignableFrom(method.ReturnType)) ? Task.FromResult(result) : result;
                    return;
                }
                //去执行当前的方法
                invocation.Proceed();
                //存入缓存
                if (!string.IsNullOrWhiteSpace(cacheKey))
                {
                    object response;

                    var type = invocation.Method.ReturnType;
                    if (typeof(Task).IsAssignableFrom(type))
                    {
                        var resultProperty = type.GetProperty("Result");
                        response = resultProperty.GetValue(invocation.ReturnValue);
                    }
                    else
                    {
                        response = invocation.ReturnValue;
                    }
                    if (response == null) response = string.Empty;

                    _cache.Set(cacheKey, response, TimeSpan.FromMinutes(cachingAttribute.AbsoluteExpiration));
                }
            }
            else
            {
                invocation.Proceed();//直接执行被拦截方法
            }
        }
    }
}
