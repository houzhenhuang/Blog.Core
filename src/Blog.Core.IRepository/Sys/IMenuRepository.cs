﻿using Blog.Core.IRepository.Base;
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
    public interface IMenuRepository : IBaseRepository<SysMenu>
    {
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        Task<ISugarQueryable<MenuDto>> GetMenus();
    }
}
