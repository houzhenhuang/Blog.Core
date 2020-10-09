using Blog.Core.Model.Core;
using SqlSugar;

namespace Blog.Core.Model.Models
{
    /// <summary>
    /// 用户角色表
    /// </summary>
    public class SysUserRole : Entity
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 角色id
        /// </summary>
        public int RoleId { get; set; }
    }
}
