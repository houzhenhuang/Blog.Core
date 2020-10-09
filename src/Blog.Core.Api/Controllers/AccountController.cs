using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blog.Core.Api.AuthHelper;
using Blog.Core.Api.Models;
using Blog.Core.Api.Services.Requirements.PermissionPolicy;
using Blog.Core.Common.Helper;
using Blog.Core.IServices;
using Blog.Core.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Blog.Core.Api.Controllers
{
    /// <summary>
    /// 账号接口
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly JwtHelper _jwtHelper;
        private readonly PermissionRequirement _requirement;
        private readonly IUserServices _userServices;
        private readonly IRoleServices _roleServices;
        private readonly IUserRoleServices _userRoleServices;
        private readonly IPermissionServices _permissionServices;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="jwtHelper"></param>
        /// <param name="requirement"></param>
        /// <param name="userServices"></param>
        /// <param name="roleServices"></param>
        /// <param name="userRoleServices"></param>
        /// <param name="permissionServices"></param>
        public AccountController(ILogger<WeatherForecastController> logger, JwtHelper jwtHelper, PermissionRequirement requirement,
            IUserServices userServices, IRoleServices roleServices, IUserRoleServices userRoleServices,
            IPermissionServices permissionServices)
        {
            _logger = logger;
            _jwtHelper = jwtHelper;
            _requirement = requirement;
            _userServices = userServices;
            _roleServices = roleServices;
            _userRoleServices = userRoleServices;
            _permissionServices = permissionServices;
        }
        /// <summary>
        /// 获取token
        /// </summary>
        /// <returns></returns>
        [HttpGet("Token")]
        public async Task<ActionResult> GetToken(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                throw new UserOperationException("请输入用户名!");
            }
            if (string.IsNullOrWhiteSpace(password))
            {
                throw new UserOperationException("请输入密码!");
            }
            _logger.LogInformation("获取token...");
            var user = (await _userServices.Query(u => u.UserName == userName, MD5Helper.MD5Encrypt32(password))).SingleOrDefault();
            if (user == null)
            {
                throw new UserOperationException("用户不存在或密码错误!");
            }
            var token = _jwtHelper.IssueJwt(new TokenModelJwt { UserId = user.Id });
            _logger.LogInformation($"token结果:{token}");
            return Ok(new
            {
                token
            });
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginInput input)
        {
            if (string.IsNullOrWhiteSpace(input.UserName))
            {
                throw new UserOperationException("请输入用户名!");
            }
            if (string.IsNullOrWhiteSpace(input.Password))
            {
                throw new UserOperationException("请输入密码!");
            }
            var user = (await _userServices.Query(u => u.UserName == input.UserName && u.Password == MD5Helper.MD5Encrypt32(input.Password))).SingleOrDefault();
            if (user == null)
            {
                throw new UserOperationException("用户不存在或密码错误!");
            }
            if (user.Status == 0)
            {
                throw new UserOperationException("您的账号已被禁用!");
            }
            var tokenModel = new TokenModelJwt { UserId = user.Id };
            var userRoles = await _userRoleServices.Query(ur => ur.UserId == user.Id);
            if (userRoles.Any())
            {
                var roleIds = userRoles.Select(ur => ur.RoleId).ToList();
                var roles = await _roleServices.Query(r => roleIds.Contains(r.Id));
                tokenModel.Roles = roles.Select(r => r.Name).ToList();
            }

            var userPermissions = await _permissionServices.GetUserPermissions(user.Id);
            _requirement.Permissions = userPermissions.Select(p => new Permission
            {
                Role = p.RoleName,
                Url = p.LinkUrl
            }).ToList();

            var token = _jwtHelper.BuildJwtToken(tokenModel);
            return Ok(token);
        }
        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("RefreshToken")]
        public async Task<IActionResult> RefreshToken(string token = "")
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new UserOperationException("token无效,请重新登录!");
            }
            var tokenModel = _jwtHelper.SerializeJwt(token);
            if (tokenModel == null)
            {
                throw new UserOperationException("错误token!");
            }
            var tokenDto = _jwtHelper.BuildJwtToken(tokenModel);
            return Ok(tokenDto);
        }
    }
    public class LoginInput
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
