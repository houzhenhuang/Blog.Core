using Blog.Core.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.ViewModels.Permission
{
    public class AddMenuViewModel
    {
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
        /// 路由地址
        /// </summary>
        public string Route { get; set; }
        /// <summary>
        /// api资源Id
        /// </summary>
        public int ApiResourceId { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Remark { get; set; }
    }
}
