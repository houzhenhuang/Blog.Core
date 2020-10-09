using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.ViewModels.Role
{
    public class AddRoleViewModel
    {
        /// <summary>
        /// 角色名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
