using Blog.Core.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.Dtos.Permission
{
    public class MenuDto
    {
        /// <summary>
        /// pk
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 父id
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 标识码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 权限类型
        /// </summary>
        public int TypeId { get; set; }
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 路由地址
        /// </summary>
        public string Route { get; set; }
        /// <summary>
        /// api资源Id
        /// </summary>
        public int ApiResourceId { get; set; }
        /// <summary>
        /// api地址
        /// </summary>
        public string LinkUrl { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int Creator { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = new DateTime();
        /// <summary>
        /// 描述
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 父菜单节点数组
        /// </summary>
        public List<int> ParentIds { get; set; } = new List<int>();
        /// <summary>
        /// 子菜单
        /// </summary>
        public List<MenuDto> Children { get; set; }

    }
}
