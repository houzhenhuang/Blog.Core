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
    public interface IPermissionMenuServices : IBaseServices<SysPermissionMenu>
    {
        /// <summary>
        /// 获取用户权限菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<IList<PermissionMenuDto>> GetPermissionMenus(int userId);
    }
}
