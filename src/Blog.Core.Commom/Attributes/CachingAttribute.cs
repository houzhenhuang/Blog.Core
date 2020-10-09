using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class CachingAttribute : Attribute
    {
        public CachingAttribute()
        {

        }
        /// <summary>
        /// 缓存绝对过期时间（分钟）
        /// </summary>
        public int AbsoluteExpiration { get; set; } = 30;
    }
}
