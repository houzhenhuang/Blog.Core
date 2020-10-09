using Blog.Core.Model.Core;
using SqlSugar;
using System;

namespace Blog.Core.Model.Models
{
    /// <summary>
    /// 角色表
    /// </summary>
    public class SysRole : BaseEntity
    {
        /// <summary>
        /// 角色名
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200)]
        public string Name { get; set; }
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool Enabled { get; set; }

    }
}
