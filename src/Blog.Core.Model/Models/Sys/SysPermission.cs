using Blog.Core.Model.Core;
using SqlSugar;
using System;

namespace Blog.Core.Model.Models
{
    /// <summary>
    /// 权限表
    /// </summary>
    public class SysPermission : BaseEntity
    {
        /// <summary>
        /// 权限类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }
    }
}
