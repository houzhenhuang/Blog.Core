using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.Dtos.User
{
    public class UserDto
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public int Sex { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        /// 用户状态
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int Creator { get; set; }
        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public List<string> Roles { get; set; }
    }
}
