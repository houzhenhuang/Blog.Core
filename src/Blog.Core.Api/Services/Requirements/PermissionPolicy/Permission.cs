using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Api.Services.Requirements.PermissionPolicy
{
    public class Permission
    {
        /// <summary>
        /// 角色
        /// </summary>
        public virtual string Role { get; set; }
        /// <summary>
        /// 请求url
        /// </summary>
        public virtual string Url { get; set; }
    }
}
