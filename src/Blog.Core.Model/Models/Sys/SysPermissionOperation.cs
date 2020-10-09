using Blog.Core.Model.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.Models
{
    /// <summary>
    /// 权限-功能操作关系表
    /// </summary>
    public class SysPermissionOperation : Entity
    {
        /// <summary>
        /// 权限Id
        /// </summary>
        public int PermissionId { get; set; }
        /// <summary>
        /// 操作Id
        /// </summary>
        public int OperationId { get; set; }
    }
}
