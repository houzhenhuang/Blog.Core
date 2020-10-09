using Blog.Core.IRepository.Base;
using Blog.Core.Model.Dtos;
using Blog.Core.Model.Dtos.Permission;
using Blog.Core.Model.Enums;
using Blog.Core.Model.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blog.Core.IRepository
{
    public interface IPermissionRepository : IBaseRepository<SysPermission>
    {
        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ISugarQueryable<UserPermissionDto>> GetUserPermissions(int userId);
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        Task<ISugarQueryable<MenuDto>> GetMenus();
    }
}
