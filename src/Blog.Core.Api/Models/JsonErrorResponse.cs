using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Api.Models
{
    /// <summary>
    /// 返回错误信息
    /// </summary>
    public class JsonErrorResponse
    {
        /// <summary>
        /// 生产环境的消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 开发环境异常消息
        /// </summary>
        public object DeveloperMessage { get; set; }
    }
}
