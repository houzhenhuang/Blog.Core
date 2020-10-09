using AutoMapper;
using Blog.Core.Api.Models;
using Blog.Core.Api.Services.Requirements.PermissionPolicy;
using Blog.Core.IRepository.UnitOfWork;
using Blog.Core.IServices;
using Blog.Core.Model;
using Blog.Core.Model.Dtos;
using Blog.Core.Model.Dtos.Permission;
using Blog.Core.Model.Models;
using Blog.Core.Model.ViewModels.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Blog.Core.Api.Controllers
{
    /// <summary>
    /// 权限接口
    /// </summary>
    public class PermissionController : BaseController
    {
        private readonly IPermissionServices _permissionServices;
        private readonly IPermissionMenuServices _permissionMenuServices;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PermissionController(IPermissionServices permissionServices, IPermissionMenuServices permissionMenuServices,
            IUnitOfWork unitOfWork, IMapper mapper)
        {
            _permissionServices = permissionServices;
            _permissionMenuServices = permissionMenuServices;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        /// <summary>
        /// 获取用户权限菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetPermissionMenus")]
        [Authorize]
        public async Task<IActionResult> GetPermissionMenus()
        {
            var permisMenus = await _permissionMenuServices.GetPermissionMenus(UserIdentity.UserId);
            return Ok(new JsonResponse<IList<PermissionMenuDto>> { Data = permisMenus });
        }
        /// <summary>
        /// 获取全部权限列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetPermissions")]
        [ProducesResponseType(typeof(List<PermissionDto>), (int)HttpStatusCode.OK)]
        [Authorize(nameof(Permission))]
        public async Task<IActionResult> GetPermissions()
        {
            var permissions = await _permissionServices.GetPermissions();
            return Ok(new JsonResponse<List<PermissionDto>> { Data = permissions });
        }
    }
}