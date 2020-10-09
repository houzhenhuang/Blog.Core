using Blog.Core.Model.Core;
using SqlSugar;
using System;

namespace Blog.Core.Model.Models
{
    /// <summary>
    ///  api资源表
    /// </summary>
    public class SysApiResource : BaseEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 标识
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string Code { get; set; }
        /// <summary>
        /// api地址
        /// </summary>
        public string LinkUrl { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
