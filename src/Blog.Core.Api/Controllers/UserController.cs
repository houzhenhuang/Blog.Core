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
using Blog.Core.Model.Dtos.User;
using Blog.Core.Model.Models;
using Blog.Core.Model.ViewModels.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Blog.Core.Api.Controllers
{
    /// <summary>
    /// 用户接口
    /// </summary>

    public class UserController : BaseController
    {
        private readonly ILogger<UserController> _logger;
        private readonly JwtHelper _jwtHelper;
        private readonly IUserServices _userServices;
        private readonly IRoleServices _roleServices;
        private readonly IUserRoleServices _userRoleServices;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
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
        public UserController(ILogger<UserController> logger, JwtHelper jwtHelper, IMapper mapper,
            IUserServices userServices, IRoleServices roleServices, IUserRoleServices userRoleServices, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _jwtHelper = jwtHelper;
            _mapper = mapper;
            _userServices = userServices;
            _roleServices = roleServices;
            _userRoleServices = userRoleServices;
            _unitOfWork = unitOfWork;
        }
        ///// <summary>
        ///// 获取用户列表
        ///// </summary>
        ///// <returns></returns>
        //[HttpGet("GetUsers")]
        //[Authorize(nameof(Permission))]
        //[ProducesResponseType(typeof(IList<UserDto>), (int)HttpStatusCode.OK)]
        //public async Task<IActionResult> GetUsers(string keyworld = "")
        //{
        //    keyworld = string.IsNullOrEmpty(keyworld) ? "" : keyworld;
        //    var users = await _userServices.Query(p => p.UserName.Contains(keyworld));
        //    return Ok(new JsonResponse<IList<UserDto>>()
        //    {
        //        Data = _mapper.Map<IList<UserDto>>(users)
        //    });
        //}
        /// <summary>
        /// 获取用户列表(分页)
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUsersByPage")]
        [ProducesResponseType(typeof(PageResult<UserDto>), (int)HttpStatusCode.OK)]
        [Authorize(nameof(Permission))]
        public async Task<IActionResult> GetUsersByPage(int pageIndex = 1, string keyworld = "")
        {
            keyworld = string.IsNullOrEmpty(keyworld) ? "" : keyworld;
            var pageResult = await _userServices.QueryByPage(pageIndex, 10, "", p => p.UserName.Contains(keyworld));
            var users = _mapper.Map<List<UserDto>>(pageResult.Data);
            var roles = await _roleServices.GetRoles();
            foreach (var user in users)
            {
                user.Roles = roles.Where(r => r.UserId == user.Id).Select(r => r.Name).ToList();
            }
            return Ok(new JsonResponse<PageResult<UserDto>>
            {
                Data = new PageResult<UserDto>()
                {
                    TotalCount = pageResult.TotalCount,
                    PageIndex = pageResult.PageIndex,
                    PageCount = pageResult.PageCount,
                    PageSize = pageResult.PageSize,
                    Data = users
                }
            });
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost("AddUser")]
        [ProducesResponseType(typeof(JsonResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [Authorize(nameof(Permission))]
        public async Task<JsonResponse> AddUser([FromBody] AddUserViewModel vm)
        {
            var user = (await _userServices.Query(p => p.UserName == vm.UserName)).SingleOrDefault();
            if (user != null)
            {
                throw new UserOperationException("该帐号已存在");
            }
            vm.Password = MD5Helper.MD5Encrypt32(vm.Password);
            user = _mapper.Map<SysUser>(vm);
            user.Creator = UserIdentity.UserId;
            user.Remark = "添加用户";
            user.CreateTime = DateTime.Now;
            user.Status = 1;
            var result = _userServices.Add(user);
            if (result <= 0)
            {
                throw new UserOperationException("添加失败");
            }
            return new JsonResponse(true);
        }
        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="vm"></param>
        [HttpPut("EditUser")]
        [Authorize(nameof(Permission))]
        public async Task<JsonResponse> EditUser([FromBody] EditUserViewModel vm)
        {
            var user = await _userServices.QueryById(vm.Id);
            if (user == null)
            {
                throw new UserOperationException("用户不存在");
            }
            user.RealName = vm.RealName;
            user.UserName = vm.UserName;
            user.Status = vm.Status;
            user.Age = vm.Age;
            user.Sex = vm.Sex;
            user.Birthday = vm.Birthday;
            var uResult = await _userServices.Update(user);
            if (uResult <= 0)
            {
                throw new UserOperationException("更新失败");
            }
            return new JsonResponse(true);
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("DeleteUser")]
        [Authorize(nameof(Permission))]
        public async Task<JsonResponse> DeleteUser(int id)
        {
            var user = await _userServices.QueryById(id);
            if (user == null)
            {
                throw new UserOperationException("用户不存在");
            }
            var dResult = _userServices.DeleteById(id);
            if (dResult <= 0)
            {
                throw new UserOperationException("删除失败");
            }
            return new JsonResponse(true);
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUserInfo")]
        [Authorize]
        public async Task<IActionResult> GetUserInfo()
        {
            var user = await _userServices.QueryById(UserIdentity.UserId);
            if (user == null)
            {
                throw new Exception("用户不存在");
            }
            return Ok(new JsonResponse<UserInfoDto>
            {
                Data = new UserInfoDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    RealName = user.RealName,
                    Sex = user.Sex,
                    Avatar = user.Avatar
                }
            });
        }

    }
}
