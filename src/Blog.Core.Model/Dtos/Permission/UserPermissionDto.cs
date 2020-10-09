using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Model.Dtos.Permission
{
    public class UserPermissionDto
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string LinkUrl { get; set; }
    }
}
