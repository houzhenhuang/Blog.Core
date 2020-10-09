using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.Dtos.Permission
{
    public class PermissionDto
    {
        /// <summary>
        /// pk
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 菜单id
        /// </summary>
        public int MenuId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 父菜单id
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// 功能操作集合
        /// </summary>
        public List<PermissionDto> Operations { get; set; }
        /// <summary>
        /// 子菜单
        /// </summary>
        public List<PermissionDto> Children { get; set; }
    }
}
