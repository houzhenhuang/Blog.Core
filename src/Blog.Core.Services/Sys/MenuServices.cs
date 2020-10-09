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
using System.Threading.Tasks;

namespace Blog.Core.Services
{
    public class MenuServices : BaseServices<SysMenu>, IMenuServices
    {
        private readonly IMenuRepository _menuRepository;
        private readonly IMapper _mapper;
        public MenuServices(IMenuRepository menuRepository, IBaseRepository<SysMenu> baseRepository, IMapper mapper)
            : base(baseRepository)
        {
            _menuRepository = menuRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        public async Task<IList<MenuDto>> GetMenus()
        {
            var menus = (await _menuRepository.GetMenus()).ToList();

            var parentMenus = menus.Where(p => p.ParentId == 0).ToList();
            RecursionChildrenMenu(menus, parentMenus);
            return parentMenus;
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
