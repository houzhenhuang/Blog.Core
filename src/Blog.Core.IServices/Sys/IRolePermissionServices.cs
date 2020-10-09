using Blog.Core.IServices.Base;
using Blog.Core.Model.Dtos;
using Blog.Core.Model.Dtos.RolePermission;
using Blog.Core.Model.Models;
using Blog.Core.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blog.Core.IServices
{
    public interface IRolePermissionServices : IBaseServices<SysRolePermission>
    {
        /// <summary>
        /// 根据角色获取权限id列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        Task<IList<int>> GetPermissionIds(int roleId);
        /// <summary>
        /// 获取角色权限 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<RolePermissionDto> GetRolePermissions(int roleId);
    }
}
