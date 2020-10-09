using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Api.Services.Requirements.PermissionPolicy
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="deniedAction">无权限action</param>
        /// <param name="permissions">权限集合</param>
        public PermissionRequirement(string deniedAction, List<Permission> permissions)
        {
            DeniedAction = deniedAction;
            Permissions = permissions;
        }
        /// <summary>
        /// 权限集合
        /// </summary>
        public List<Permission> Permissions { get; set; }
        /// <summary>
        /// 无权限时跳转的action
        /// </summary>
        public string DeniedAction { get; set; }
    }
}
