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
    [Authorize(nameof(Permission))]
    public class MenuController : BaseController
    {
        private readonly IPermissionServices _permissionServices;
        private readonly IPermissionMenuServices _permissionMenuServices;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MenuController(IPermissionServices permissionServices, IPermissionMenuServices permissionMenuServices,
            IUnitOfWork unitOfWork, IMapper mapper)
        {
            _permissionServices = permissionServices;
            _permissionMenuServices = permissionMenuServices;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        /// <summary>
        /// 获取全部菜单列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetMenus")]
        [ProducesResponseType(typeof(List<MenuDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMenus()
        {
            var menuus = await _permissionServices.GetMenus();
            return Ok(new JsonResponse<IList<MenuDto>> { Data = menuus });
        }
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost("AddMenu")]
        public async Task<JsonResponse> AddMenu([FromBody] AddMenuViewModel vm)
        {
            var menu = _mapper.Map<SysPermission>(vm);
            menu.Creator = UserIdentity.UserId;
            menu.CreateTime = DateTime.Now;
            menu.ModifyTime = DateTime.Now;

            var mResult = await _permissionServices.AddAsync(menu);
            if (mResult <= 0)
            {
                throw new UserOperationException("添加失败");
            }
            return new JsonResponse(true);
        }
        /// <summary>
        /// 更新菜单
        /// </summary>
        /// <param name="vm"></param>
        [HttpPut("EditMenu")]
        public async Task<JsonResponse> EditMenu([FromBody] EditMenuViewModel vm)
        {
            var menu = await _permissionServices.QueryById(vm.Id);
            if (menu == null)
            {
                throw new UserOperationException("菜单不存在");
            }
            var module = _mapper.Map<SysPermission>(vm);

            module.Creator = menu.Creator;
            module.CreateTime = menu.CreateTime;
            module.ModifyTime = DateTime.Now;

            var mResult = await _permissionServices.Update(module);
            if (mResult <= 0)
            {
                throw new UserOperationException("更新失败");
            }
            return new JsonResponse(true);
        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("DeleteMenu")]
        public async Task<JsonResponse> DeleteMenu(int id)
        {
            var module = await _permissionServices.QueryById(id);
            if (module == null)
            {
                throw new UserOperationException("菜单不存在");
            }
            var puResult = await _permissionServices.DeleteByIdAsync(id);
            if (puResult <= 0)
            {
                throw new UserOperationException("删除失败");
            }
            return new JsonResponse(true);
        }
    }
}