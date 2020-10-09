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
    public interface IMenuServices : IBaseServices<SysMenu>
    {
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        Task<IList<MenuDto>> GetMenus();
    }
}
