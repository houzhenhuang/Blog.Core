using Blog.Core.Model.Core;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.Models
{
    /// <summary>
    /// 菜单表
    /// </summary>
    public class SysMenu : BaseEntity
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 父菜单id
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// 菜单路由地址(菜单url)
        /// </summary>
        public string Route { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string Icon { get; set; }
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
