using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.ViewModels.Role
{
    public class EditRoleViewModel
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
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
