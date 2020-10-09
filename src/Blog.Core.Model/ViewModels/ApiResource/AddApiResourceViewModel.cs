using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.ViewModels.ApiResource
{
    public class AddApiResourceViewModel
    {
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
        /// 描述
        /// </summary>
        public string Remark { get; set; }
    }
}
