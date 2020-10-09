using Blog.Core.IServices.Base;
using Blog.Core.Model.Dtos.Role;
using Blog.Core.Model.Models;
using Blog.Core.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blog.Core.IServices
{
    public interface IRoleServices : IBaseServices<SysRole>
    {
        Task<List<RoleDto>> GetRoles();
        Task<List<string>> GetRoleNamesByUserId(int userId);
    }
}
