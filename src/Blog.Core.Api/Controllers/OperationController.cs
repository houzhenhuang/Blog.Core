using AutoMapper;
using Blog.Core.Api.Models;
using Blog.Core.Api.Services.Requirements.PermissionPolicy;
using Blog.Core.IRepository;
using Blog.Core.IRepository.UnitOfWork;
using Blog.Core.IServices;
using Blog.Core.Model;
using Blog.Core.Model.Consts;
using Blog.Core.Model.Dtos;
using Blog.Core.Model.Dtos.Operation;
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
    /// 功能操作接口
    /// </summary>
    [Authorize(nameof(Permission))]
    public class OperationController : BaseController
    {
        private readonly IOperationServices _operationServices;
        private readonly IPermissionServices _permissionServices;
        private readonly IPermissionOperationServices _permissionOperationServices;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public OperationController(IOperationServices operationServices,
            IPermissionServices permissionServices, IPermissionOperationServices permissionOperationServices,
            IUnitOfWork unitOfWork, IMapper mapper)
        {
            _operationServices = operationServices;
            _permissionServices = permissionServices;
            _permissionOperationServices = permissionOperationServices;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        /// <summary>
        /// 获取菜单操作功能
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetOperationsByMenuId")]
        [ProducesResponseType(typeof(List<OperationDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOperationsByMenuId(int menuId)
        {
            var operations = await _operationServices.GetOperationsByMenuId(menuId);

            return Ok(new JsonResponse<List<OperationDto>> { Data = operations });
        }
        /// <summary>
        /// 添加菜单功能
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("AddOperation")]
        public async Task<JsonResponse> AddOperation([FromBody] AddOperationDto model)
        {
            var operation = _mapper.Map<SysOperation>(model);
            operation.ParentId = 0;
            operation.Creator = UserIdentity.UserId;
            operation.CreateTime = DateTime.Now;
            operation.ModifyTime = DateTime.Now;

            _unitOfWork.Begin();

            var operationId = await _operationServices.AddAsync(operation);

            var permissionId = await _permissionServices.AddAsync(new SysPermission
            {
                Name = operation.Name,
                Type = SysConst.OPERATION,
                Remark = operation.Remark,
                Creator = UserIdentity.UserId,
                CreateTime = DateTime.Now,
                Reviser = UserIdentity.UserId,
                ModifyTime = DateTime.Now
            });

            var permisOperaId = await _permissionOperationServices.AddAsync(new SysPermissionOperation
            {
                OperationId = operationId,
                PermissionId = permissionId
            });

            if (operationId <= 0 || permissionId <= 0 || permisOperaId <= 0)
            {
                _unitOfWork.Rollback();
                throw new UserOperationException("添加失败");
            }
            _unitOfWork.Commit();
            return new JsonResponse(true);
        }
        /// <summary>
        /// 更新菜单功能
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("EditOperation")]
        public async Task<JsonResponse> EditOperation([FromBody] EditOperationDto model)
        {
            var operation = await _operationServices.QueryById(model.Id);
            if (operation == null)
            {
                throw new UserOperationException("该功能不存在");
            }
            var editModel = _mapper.Map<SysOperation>(model);

            editModel.Creator = operation.Creator;
            editModel.CreateTime = operation.CreateTime;
            editModel.ModifyTime = DateTime.Now;
            editModel.Reviser = UserIdentity.UserId;
            editModel.Sort = operation.Sort;
            editModel.ParentId = operation.ParentId;

            var mResult = await _operationServices.Update(editModel);
            if (mResult <= 0)
            {
                throw new UserOperationException("更新失败");
            }
            return new JsonResponse(true);
        }
        /// <summary>
        /// 删除菜单功能
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("DeleteOperation")]
        public async Task<JsonResponse> DeleteOperation(int id)
        {
            var operation = await _operationServices.QueryById(id);
            if (operation == null)
            {
                throw new UserOperationException("该功能不存在");
            }
            _unitOfWork.Begin();

            var delOperaResult = await _operationServices.DeleteByIdAsync(id);
            var permisOperation = (await _permissionOperationServices.Query(po => po.OperationId == operation.Id)).SingleOrDefault();
            if (permisOperation != null)
            {
                if ((await _permissionOperationServices.DeleteByIdAsync(permisOperation.Id)) <= 0)
                {
                    _unitOfWork.Rollback();
                    throw new UserOperationException("删除失败");
                }
                var permission = (await _permissionServices.Query(p => p.Id == permisOperation.PermissionId)).SingleOrDefault();
                if (permission != null)
                {
                    if ((await _permissionServices.DeleteByIdAsync(permission.Id)) <= 0)
                    {
                        _unitOfWork.Rollback();
                        throw new UserOperationException("删除失败");
                    }
                }
            }
            if (delOperaResult <= 0)
            {
                _unitOfWork.Rollback();
                throw new UserOperationException("删除失败");
            }
            _unitOfWork.Commit();
            return new JsonResponse(true);
        }
    }
}