using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Core.Api.AuthHelper;
using Blog.Core.Api.Models;
using Blog.Core.Api.Services.Requirements.PermissionPolicy;
using Blog.Core.Common.Helper;
using Blog.Core.IRepository.UnitOfWork;
using Blog.Core.IServices;
using Blog.Core.Model;
using Blog.Core.Model.Dtos;
using Blog.Core.Model.Dtos.Role;
using Blog.Core.Model.Dtos.User;
using Blog.Core.Model.Models;
using Blog.Core.Model.ViewModels.User;
using Blog.Core.Model.ViewModels.UserRole;
using Blog.Core.Repository.Sugar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SqlSugar;

namespace Blog.Core.Api.Controllers
{
    /// <summary>
    /// 用户角色接口
    /// </summary>

    [Authorize(nameof(Permission))]
    public class UserRoleController : BaseController
    {
        private readonly IUserServices _userServices;
        private readonly IUserRoleServices _userRoleServices;
        private readonly IRoleServices _roleServices;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="jwtHelper"></param>
        /// <param name="mapper"></param>
        /// <param name="userServices"></param>
        /// <param name="roleServices"></param>
        /// <param name="userRoleServices"></param>
        /// <param name="unitOfWork"></param>
        public UserRoleController(IUserServices userServices, IUserRoleServices userRoleServices, IRoleServices roleServices, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _userServices = userServices;
            _userRoleServices = userRoleServices;
            _roleServices = roleServices;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// 为用户分配角色
        /// </summary>
        /// <param name="vm"></param>
        [HttpPatch("DisRole")]
        public async Task<JsonResponse> DisRole([FromBody] DisRoleViewModel vm)
        {
            var user = await _userServices.QueryById(vm.UserId);
            if (user == null)
            {
                throw new UserOperationException("用户不存在");
            }
            _unitOfWork.Begin();
            var userRoles = (await _userRoleServices.Query(p => p.UserId == vm.UserId)).ToList();
            var removeCount = 0;
            var addCount = 0;
            foreach (var userRole in userRoles)
            {
                removeCount += await _userRoleServices.DeleteByIdAsync(userRole.Id);
            }
            foreach (var roleId in vm.RoleIds)
            {
                var userRoleId = await _userRoleServices.AddAsync(new SysUserRole { UserId = vm.UserId, RoleId = roleId });
                addCount += userRoleId > 0 ? 1 : 0;
            }
            if (!(removeCount == userRoles.Count && addCount == vm.RoleIds.Count))
            {
                _unitOfWork.Rollback();
                throw new UserOperationException("操作失败");
            }
            _unitOfWork.Commit();
            return new JsonResponse(true);
        }

        [HttpGet("GetRolesByUser")]
        public async Task<JsonResponse<List<RoleDto>>> GetRolesByUser(int userId)
        {
            var roles = await _userRoleServices.GetRoleByUserId(userId);

            return new JsonResponse<List<RoleDto>> { Data = _mapper.Map<List<RoleDto>>(roles) };
        }
    }
}
