using AutoMapper;
using Blog.Core.Common.Attributes;
using Blog.Core.Common.Comparers;
using Blog.Core.IRepository;
using Blog.Core.IRepository.Base;
using Blog.Core.IServices;
using Blog.Core.Model.Consts;
using Blog.Core.Model.Dtos;
using Blog.Core.Model.Dtos.Permission;
using Blog.Core.Model.Enums;
using Blog.Core.Model.Models;
using Blog.Core.Model.ViewModels;
using Blog.Core.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blog.Core.Services
{
    public class PermissionMenuServices : BaseServices<SysPermissionMenu>, IPermissionMenuServices
    {
        private readonly IPermissionMenuRepository _permissionMenuRepository;
        public PermissionMenuServices(IPermissionMenuRepository permissionMenuRepository,
            IBaseRepository<SysPermissionMenu> baseRepository)
            : base(baseRepository)
        {
            _permissionMenuRepository = permissionMenuRepository;
        }
        public async Task<IList<PermissionMenuDto>> GetPermissionMenus(int userId)
        {
            var userPermisMenus = (await _permissionMenuRepository.GetPermissionMenus(userId)).ToList().Distinct(new PermissionMenuComparer()).ToList();
            var parentMenu = userPermisMenus.Where(p => p.ParentId == 0).ToList();
            RecursionChildrenMenu(userPermisMenus, parentMenu);
            return parentMenu;
        }

        /// <summary>
        /// 递归子菜单项
        /// </summary>
        /// <param name="all"></param>
        /// <param name="parent"></param>
        private void RecursionChildrenMenu(List<PermissionMenuDto> all, List<PermissionMenuDto> parent)
        {
            foreach (var item in parent)
            {
                item.Meta = new PermissionMenuMeta
                {
                    Icon = item.Icon,
                    Title = item.Name
                };
                var subMenus = all.Where(p => p.ParentId == item.Id).ToList();
                item.IsMenuItem = item.ParentId == 0 && !subMenus.Any();
                foreach (var subMenu in subMenus)
                {
                    item.Children.Add(subMenu);
                }
                RecursionChildrenMenu(all, subMenus);
            }
        }

    }
}
