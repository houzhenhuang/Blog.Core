using Blog.Core.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.ViewModels.User
{
    public class EditUserViewModel
    {
        /// <summary>
        /// pk
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 密码 
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// 用户真实姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 用户状态
        /// </summary>
        public int Status { get; set; }
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
        public DateTime Birthday { get; set; }
    }
}
