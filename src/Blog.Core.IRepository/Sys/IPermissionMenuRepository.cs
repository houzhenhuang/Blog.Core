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
    public interface IPermissionMenuRepository : IBaseRepository<SysPermissionMenu>
    {
        Task<ISugarQueryable<PermissionDto>> GetPermissionMenus();
        /// <summary>
        /// 获取用户权限菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<ISugarQueryable<PermissionMenuDto>> GetPermissionMenus(int userId);
    }
}
