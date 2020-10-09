using AutoMapper;
using Blog.Core.Common.Attributes;
using Blog.Core.IRepository;
using Blog.Core.IRepository.Base;
using Blog.Core.IServices;
using Blog.Core.Model.Dtos.Role;
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
    public class RoleServices : BaseServices<SysRole>, IRoleServices
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        public RoleServices(IRoleRepository roleRepository, IBaseRepository<SysRole> baseRepository, IMapper mapper)
            : base(baseRepository)
        {
            _roleRepository = roleRepository;
            _mapper = mapper;
        }
        public async Task<List<RoleDto>> GetRoles()
        {
            return (await _roleRepository.GetRoles()).ToList();
        }
        public async Task<List<string>> GetRoleNamesByUserId(int userId)
        {
            var roles = (await _roleRepository.GetRoleByUserId(userId)).ToList();
            if (roles.Count == 0)
            {
                return new List<string>();
            }
            return roles.Select(p => p.Name).ToList();
        }
    }
}
