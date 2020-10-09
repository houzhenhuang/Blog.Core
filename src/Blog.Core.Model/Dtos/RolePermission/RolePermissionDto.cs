using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.Dtos.RolePermission
{
    public class RolePermissionDto
    {
        /// <summary>
        /// 菜单ids
        /// </summary>
        public List<int> PermissionMenuIds { get; set; }
        /// <summary>
        /// 功能ids
        /// </summary>
        public List<int> PermissionOperationIds { get; set; }
    }
}
