using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.ViewModels.UserRole
{
    public class DisRoleViewModel
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 角色ids
        /// </summary>
        public List<int> RoleIds { get; set; }
    }
}
