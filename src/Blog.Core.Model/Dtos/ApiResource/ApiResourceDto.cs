using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.Dtos.ApiResource
{
    public class ApiResourceDto
    {
        /// <summary>
        /// pk
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 标识
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// api地址
        /// </summary>
        public string LinkUrl { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = new DateTime();
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; } = new DateTime();
        /// <summary>
        /// 创建人
        /// </summary>
        public int Creator { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Remark { get; set; }
    }
}
