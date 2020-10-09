using Blog.Core.IRepository.Base;
using Blog.Core.Model.Dtos.Permission;
using Blog.Core.Model.Dtos.RolePermission;
using Blog.Core.Model.Enums;
using Blog.Core.Model.Models;
using SqlSugar;
using System.Threading.Tasks;

namespace Blog.Core.IRepository
{
    public interface IRolePermissionRepository : IBaseRepository<SysRolePermission>
    {
        /// <summary>
        /// 根据角色获取权限id列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<ISugarQueryable<int>> GetPermissionIds(int roleId);
        /// <summary>
        /// 根据角色获取权限菜单
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<ISugarQueryable<PermissionDto>> GetPermissionByRole(int roleId, string permissionType);
    }
}
