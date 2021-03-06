﻿using Blog.Core.IRepository.Base;
using Blog.Core.Model.Models;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blog.Core.IRepository
{
    public interface IUserRoleRepository : IBaseRepository<SysUserRole>
    {
        Task<ISugarQueryable<SysRole>> GetRoleByUserId(int userId);
    }
}
