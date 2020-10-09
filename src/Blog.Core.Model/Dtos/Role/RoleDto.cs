using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.Dtos.Role
{
    public class RoleDto
    {
        /// <summary>
        /// pk
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 角色名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool Enabled { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public int Creator { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; } = new DateTime();
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; } = new DateTime();
        /// <summary>
        /// 描述
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get; set; }
    }
}
