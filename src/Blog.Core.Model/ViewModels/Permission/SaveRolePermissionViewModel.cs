using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.ViewModels.Permission
{
    public class SaveRolePermissionViewModel
    {
        public int RoleId { get; set; }
        public List<int> PermissionIds { get; set; }
    }
}
