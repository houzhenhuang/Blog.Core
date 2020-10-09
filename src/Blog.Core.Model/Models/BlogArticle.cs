using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.Models
{
    public class BlogArticle
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(IsNullable = false, IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [SugarColumn(Length = 60, IsNullable = true)]
        public string Submitter { get; set; }

        /// <summary>
        /// 标题blog
        /// </summary>
        [SugarColumn(Length = 256, IsNullable = true)]
        public string Title { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        [SugarColumn(Length = int.MaxValue, IsNullable = true)]
        public string Category { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [SugarColumn(IsNullable = true, ColumnDataType = "text")]
        public string Content { get; set; }

        /// <summary>
        /// 访问量
        /// </summary>
        public int Traffic { get; set; }

        /// <summary>
        /// 评论数量
        /// </summary>
        public int CommentNum { get; set; }

        /// <summary> 
        /// 修改时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(Length = int.MaxValue, IsNullable = true)]
        public string Remark { get; set; }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        [SugarColumn(IsNullable = true)]
        public bool IsDeleted { get; set; }
    }
}
