using Blog.Core.Model.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.Models
{
    /// <summary>
    /// 功能操作表
    /// </summary>
    public class SysOperation : BaseEntity
    {
        /// <summary>
        /// 操作名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 操作编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 所属菜单Id
        /// </summary>
        public int MenuId { get; set; }
        /// <summary>
        /// 接口id
        /// </summary>
        public int ApiResourceId { get; set; }
        /// <summary>
        /// 父操作id
        /// </summary>
        public int ParentId { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
