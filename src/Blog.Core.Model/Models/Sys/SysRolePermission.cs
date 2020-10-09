using Blog.Core.Model.Core;
using SqlSugar;

namespace Blog.Core.Model.Models
{
    /// <summary>
    /// 角色权限表
    /// </summary>
    public class SysRolePermission : Entity
    {
        /// <summary>
        /// 角色id
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// 权限id
        /// </summary>
        public int PermissionId { get; set; }

        [SugarColumn(IsIgnore = true)]
        public SysRole Role { get; set; }
        [SugarColumn(IsIgnore = true)] 
        public SysPermission Permission { get; set; }
    }
}
