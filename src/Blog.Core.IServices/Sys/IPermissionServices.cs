using Blog.Core.IServices.Base;
using Blog.Core.Model.Dtos;
using Blog.Core.Model.Dtos.Permission;
using Blog.Core.Model.Models;
using Blog.Core.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blog.Core.IServices
{
    public interface IPermissionServices : IBaseServices<SysPermission>
    {
        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<List<UserPermissionDto>> GetUserPermissions(int userId);
        /// <summary>
        /// 获取权限列表
        /// </summary>
        /// <returns></returns>
        Task<List<PermissionDto>> GetPermissions();
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        Task<List<MenuDto>> GetMenus();
    }
}
