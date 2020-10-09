using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.Core
{
    public class BaseEntity : Entity
    {
        /// <summary>
        /// 创建人
        /// </summary>
        public int Creator { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = new DateTime();
        /// <summary>
        /// 修改人
        /// </summary>
        public int Reviser { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; } = new DateTime();
        /// <summary>
        /// 描述
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public string Remark { get; set; }
    }
}
