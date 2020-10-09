using AutoMapper;
using Blog.Core.Api.Models;
using Blog.Core.Api.Services.Requirements.PermissionPolicy;
using Blog.Core.IServices;
using Blog.Core.Model;
using Blog.Core.Model.Dtos.Role;
using Blog.Core.Model.Dtos.User;
using Blog.Core.Model.Models;
using Blog.Core.Model.ViewModels.Role;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Core.Api.Controllers
{
    [Authorize(nameof(Permission))]
    public class RoleController : BaseController
    {
        private readonly IRoleServices _roleServices;
        private readonly IMapper _mapper;
        public RoleController(IRoleServices roleServices, IMapper mapper)
        {
            _mapper = mapper;
            _roleServices = roleServices;
        }

        /// <summary>
        /// 分页获取角色列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetRolesByPage")]
        public async Task<IActionResult> GetRolesByPage(int pageIndex = 1, string keyworld = "")
        {
            keyworld = string.IsNullOrEmpty(keyworld) ? "" : keyworld;
            var pageResult = await _roleServices.QueryByPage(pageIndex, 10, "", p => p.Name.Contains(keyworld));
            return Ok(new JsonResponse<PageResult<RoleDto>>
            {
                Data = new PageResult<RoleDto>()
                {
                    TotalCount = pageResult.TotalCount,
                    PageIndex = pageResult.PageIndex,
                    PageCount = pageResult.PageCount,
                    PageSize = pageResult.PageSize,
                    Data = _mapper.Map<List<RoleDto>>(pageResult.Data)
                }
            });
        }
        /// <summary>
        /// 获取全部角色列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetRoles")]
        public async Task<IActionResult> GetRoles(string keyworld = "")
        {
            keyworld = string.IsNullOrEmpty(keyworld) ? "" : keyworld;
            var roles = await _roleServices.Query(p => p.Enabled == true && p.Name.Contains(keyworld));
            return Ok(new JsonResponse<List<RoleDto>>()
            {
                Data = roles.Select(p => new RoleDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    CreateTime = p.CreateTime
                }).ToList()
            });
        }
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole(AddRoleViewModel vm)
        {
            var role = (await _roleServices.Query(p => p.Name == vm.Name)).SingleOrDefault();
            if (role != null)
            {
                throw new UserOperationException("该角色已存在");
            }
            role = _mapper.Map<SysRole>(vm);
            role.CreateTime = DateTime.Now;
            role.Enabled = true;
            var result = _roleServices.Add(role);
            if (result <= 0)
            {
                throw new UserOperationException("添加失败");
            }
            return Ok(new JsonResponse(true));
        }
        /// <summary>
        /// 更新角色信息
        /// </summary>
        /// <param name="vm"></param>
        [HttpPut("EditRole")]
        public async Task<JsonResponse> EditRole([FromBody] EditRoleViewModel vm)
        {
            var role = (await _roleServices.Query(p => p.Id == vm.Id)).SingleOrDefault();
            if (role == null)
            {
                throw new UserOperationException("角色不存在");
            }
            role.Name = vm.Name;
            role.Remark = vm.Remark;
            role.Enabled = vm.Enabled;
            var uResult = await _roleServices.Update(role);
            if (uResult <= 0)
            {
                throw new UserOperationException("更新失败");
            }
            return new JsonResponse(true);
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("DeleteRole")]
        public async Task<JsonResponse> DeleteRole(int id)
        {
            var role = (await _roleServices.Query(p => p.Id == id)).SingleOrDefault();
            if (role == null)
            {
                throw new UserOperationException("角色不存在");
            }
            var dResult =  _roleServices.DeleteById(id);
            if (dResult <= 0)
            {
                throw new UserOperationException("删除失败");
            }
            return new JsonResponse(true);
        }
    }
}