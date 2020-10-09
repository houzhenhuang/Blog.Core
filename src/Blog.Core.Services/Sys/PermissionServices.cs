using AutoMapper;
using Blog.Core.Common.Attributes;
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
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Blog.Core.Services
{
    public class PermissionServices : BaseServices<SysPermission>, IPermissionServices
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IPermissionMenuRepository _permissionMenuRepository;
        private readonly IPermissionOperationRepository _permissionOperationRepository;
        private readonly IMapper _mapper;
        public PermissionServices(IPermissionRepository permissionRepository,
            IPermissionMenuRepository permissionMenuRepository, IPermissionOperationRepository permissionOperationRepository,
            IBaseRepository<SysPermission> baseRepository, IMapper mapper)
            : base(baseRepository)
        {
            _permissionRepository = permissionRepository;
            _permissionMenuRepository = permissionMenuRepository;
            _permissionOperationRepository = permissionOperationRepository;
            _mapper = mapper;
        }

        public async Task<List<UserPermissionDto>> GetUserPermissions(int userId)
        {
            return (await _permissionRepository.GetUserPermissions(userId)).ToList();
        }
        public async Task<List<PermissionDto>> GetPermissions()
        {
            var permissionMenus = (await _permissionMenuRepository.GetPermissionMenus()).ToList();
            var permissionOperations = (await _permissionOperationRepository.GetPermissionOperations()).ToList();
            var parentPermissions = permissionMenus.Where(p => p.ParentId == 0).ToList();
            foreach (var permissionMenu in permissionMenus)
            {
                permissionMenu.Operations = permissionOperations.Where(p => p.MenuId == permissionMenu.Id).ToList();
            }
            RecursionChildrenPermission(permissionMenus, parentPermissions);
            return parentPermissions;
        }
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<MenuDto>> GetMenus()
        {
            var menus = (await _permissionRepository.GetMenus()).ToList();

            var parentMenus = menus.Where(p => p.ParentId == 0).ToList();
            RecursionChildrenMenu(menus, parentMenus);
            return parentMenus;
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
        /// <summary>
        /// 递归子权限项
        /// </summary>
        /// <param name="all"></param>
        /// <param name="parent"></param>
        private void RecursionChildrenPermission(List<PermissionDto> all, List<PermissionDto> parent)
        {
            foreach (var item in parent)
            {
                var subMenus = all.Where(p => p.ParentId == item.MenuId).ToList();
                foreach (var subMenu in subMenus)
                {
                    item.Children = item.Children == null ? new List<PermissionDto>() : item.Children;
                    item.Children.Add(subMenu);
                }
                RecursionChildrenPermission(all, subMenus);
            }
        }

        /// <summary>
        /// 递归子菜单项
        /// </summary>
        /// <param name="all"></param>
        /// <param name="parent"></param>
        private void RecursionChildrenMenu(List<MenuDto> all, List<MenuDto> parent)
        {
            foreach (var item in parent)
            {
                var subMenus = all.Where(p => p.ParentId == item.Id).ToList();
                foreach (var subMenu in subMenus)
                {
                    item.Children = item.Children == null ? new List<MenuDto>() : item.Children;
                    item.Children.Add(subMenu);
                }
                GetAllParentIds(item, all);
                RecursionChildrenMenu(all, subMenus);
            }
        }
        private void GetAllParentIds(MenuDto item, List<MenuDto> all)
        {
            if (item.ParentId == 0)
            {
                item.ParentIds.Add(0);
            }
            var curParent = all.FirstOrDefault(p => p.Id == item.ParentId);
            while (curParent != null)
            {
                item.ParentIds.Add(curParent.Id);
                curParent = all.FirstOrDefault(p => p.Id == curParent.ParentId);
            }
            item.ParentIds.Reverse();
        }

    }
}
