using Blog.Core.Api.Models;
using Blog.Core.Api.Services.Requirements.PermissionPolicy;
using Blog.Core.IRepository.UnitOfWork;
using Blog.Core.IServices;
using Blog.Core.Model;
using Blog.Core.Model.Dtos.RolePermission;
using Blog.Core.Model.Models;
using Blog.Core.Model.ViewModels.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Blog.Core.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(nameof(Permission))]
    public class RolePermissionController : ControllerBase
    {
        private readonly IRolePermissionServices _rolePermissionServices;
        private readonly IRoleServices _roleServices;
        private readonly IUnitOfWork _unitOfWork;
        public RolePermissionController(IRolePermissionServices rolePermissionServices, IRoleServices roleServices, IUnitOfWork unitOfWork)
        {
            _rolePermissionServices = rolePermissionServices;
            _roleServices = roleServices;
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetRolePermissions")]
        [ProducesResponseType(typeof(RolePermissionDto), (int)HttpStatusCode.OK)]
        public async Task<JsonResponse<RolePermissionDto>> GetRolePermissions(int roleId)
        {
            var permission = await _rolePermissionServices.GetRolePermissions(roleId);
            return new JsonResponse<RolePermissionDto>()
            {
                Data = permission
            };
        }
        /// <summary>
        /// 保存角色权限
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("SaveRolePermission")]
        public async Task<JsonResponse> SaveRolePermission([FromBody] SaveRolePermissionViewModel vm)
        {
            var roleInfo = (await _roleServices.Query(p => p.Id == vm.RoleId)).SingleOrDefault();
            if (roleInfo == null)
            {
                throw new UserOperationException("角色不存在");
            }
            var permissionIds = await _rolePermissionServices.GetPermissionIds(roleInfo.Id);
            var removeIds = permissionIds.Where(id => !vm.PermissionIds.Contains(id)).ToList();
            _unitOfWork.Begin();
            var resultCount = 0;
            foreach (var id in removeIds)
            {
                resultCount += await _rolePermissionServices.Delete(p => p.RoleId == roleInfo.Id && p.PermissionId == id);
            }
            var addIds = vm.PermissionIds.Where(id => !permissionIds.Contains(id)).ToList();
            foreach (var id in addIds)
            {
                var rolePermisId = _rolePermissionServices.Add(new SysRolePermission { RoleId = roleInfo.Id, PermissionId = id });
                resultCount += rolePermisId > 0 ? 1 : 0;
            }
            if (resultCount != removeIds.Count + addIds.Count)
            {
                _unitOfWork.Rollback();
                throw new UserOperationException("操作失败");
            }
            _unitOfWork.Commit();
            return new JsonResponse(true);
        }
    }
}