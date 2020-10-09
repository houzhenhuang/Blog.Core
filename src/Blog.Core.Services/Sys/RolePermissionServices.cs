using AutoMapper;
using Blog.Core.Common.Attributes;
using Blog.Core.IRepository;
using Blog.Core.IRepository.Base;
using Blog.Core.IServices;
using Blog.Core.Model.Consts;
using Blog.Core.Model.Dtos;
using Blog.Core.Model.Dtos.RolePermission;
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
    public class RolePermissionServices : BaseServices<SysRolePermission>, IRolePermissionServices
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IMapper _mapper;
        public RolePermissionServices(IRolePermissionRepository rolePermissionRepository, IBaseRepository<SysRolePermission> baseRepository, IMapper mapper)
            : base(baseRepository)
        {
            _rolePermissionRepository = rolePermissionRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<IList<int>> GetPermissionIds(int roleId)
        {
            var permissionIds = await _rolePermissionRepository.GetPermissionIds(roleId);
            return permissionIds.ToList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<RolePermissionDto> GetRolePermissions(int roleId)
        {
            var permisMenus = (await _rolePermissionRepository.GetPermissionByRole(roleId, SysConst.MENU)).ToList();
            var permisOperations = (await _rolePermissionRepository.GetPermissionByRole(roleId, SysConst.OPERATION)).ToList();
            var result = new RolePermissionDto
            {
                PermissionMenuIds = permisMenus.Select(p => p.Id).ToList(),
                PermissionOperationIds = permisOperations.Select(p => p.Id).ToList()
            };
            return result;
        }
    }
}
