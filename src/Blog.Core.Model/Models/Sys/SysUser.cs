using Blog.Core.Model.Core;
using SqlSugar;
using System;

namespace Blog.Core.Model.Models
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class SysUser : BaseEntity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200)]
        public string UserName { get; set; }
        /// <summary>
        /// 密码 
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200)]
        public string Password { get; set; }
        /// <summary>
        /// 用户真实姓名
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200)]
        public string RealName { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        [SugarColumn(ColumnDataType = "nvarchar", Length = 200, IsNullable = true)]
        public string Avatar { get; set; }
        /// <summary>
        /// 用户状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        ///最后登录时间 
        /// </summary>
        public DateTime LastLoginTime { get; set; } = DateTime.Now;
        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime Birthday { get; set; } = DateTime.Now;

    }
}
