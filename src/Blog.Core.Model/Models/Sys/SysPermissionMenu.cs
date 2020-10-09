using Blog.Core.Model.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.Models
{
    /// <summary>
    /// 权限-菜单关系表
    /// </summary>
    public class SysPermissionMenu : Entity
    {
        /// <summary>
        /// 权限Id
        /// </summary>
        public int PermissionId { get; set; }
        /// <summary>
        /// 菜单Id
        /// </summary>
        public int MenuId { get; set; }
    }
}
