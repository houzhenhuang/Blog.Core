using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Blog.Core.Api.Models;
using Blog.Core.Api.Services.Requirements.PermissionPolicy;
using Blog.Core.IServices;
using Blog.Core.Model;
using Blog.Core.Model.Dtos.ApiResource;
using Blog.Core.Model.Models;
using Blog.Core.Model.ViewModels.ApiResource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace Blog.Core.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(nameof(Permission))]
    public class ApiResourceController : BaseController
    {
        private readonly IApiResourceServices _apiResourceServices;
        private readonly IOperationServices _operationServices;
        private readonly IMapper _mapper;
        public ApiResourceController(IApiResourceServices apiResourceServices, IOperationServices operationServices, IMapper mapper)
        {
            _apiResourceServices = apiResourceServices;
            _operationServices = operationServices;
            _mapper = mapper;
        }
        /// <summary>
        /// 分页获取api接口列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="keyworld"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetApiResourcesByPage")]
        [ProducesResponseType(typeof(PageResult<ApiResourceDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetApiResourcesByPage(int pageIndex = 1, string keyworld = "")
        {
            keyworld = string.IsNullOrEmpty(keyworld) ? "" : keyworld;
            var pageResult = await _apiResourceServices.QueryByPage(pageIndex, 10, "", p => p.Name.Contains(keyworld) || p.LinkUrl.Contains(keyworld));
            return Ok(new JsonResponse<PageResult<ApiResourceDto>>
            {
                Data = new PageResult<ApiResourceDto>()
                {
                    TotalCount = pageResult.TotalCount,
                    PageIndex = pageResult.PageIndex,
                    PageCount = pageResult.PageCount,
                    PageSize = pageResult.PageSize,
                    Data = _mapper.Map<List<ApiResourceDto>>(pageResult.Data)
                }
            });
        }
        /// <summary>
        /// 获取全部api接口列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetApiResources")]
        [ProducesResponseType(typeof(PageResult<ApiResourceDto>), (int)HttpStatusCode.OK)]
        public async Task<JsonResponse<List<ApiResourceDto>>> GetApiResources()
        {
            var apiResources = await _apiResourceServices.Query(null, a => a.Sort);
            var data = _mapper.Map<List<ApiResourceDto>>(apiResources);
            return new JsonResponse<List<ApiResourceDto>>()
            {
                Data = data
            };
        }
        /// <summary>
        /// 添加接口
        /// </summary>
        /// <param name="vm"></param>
        /// <returns></returns>
        [HttpPost("AddApiResource")]
        public async Task<IActionResult> AddApiResource(AddApiResourceViewModel vm)
        {
            var lowerLinkUrl = vm.LinkUrl.ToLower();
            var apiResource = (await _apiResourceServices.Query(p => p.LinkUrl == lowerLinkUrl)).SingleOrDefault();
            if (apiResource != null)
            {
                throw new UserOperationException("该接口地址已存在");
            }
            apiResource = _mapper.Map<SysApiResource>(vm);
            apiResource.CreateTime = DateTime.Now;
            apiResource.ModifyTime = DateTime.Now;
            apiResource.Creator = UserIdentity.UserId;
            var result = _apiResourceServices.Add(apiResource);
            if (result <= 0)
            {
                throw new UserOperationException("添加失败");
            }
            return Ok(new JsonResponse(true));
        }
        /// <summary>
        /// 更新接口信息
        /// </summary>
        /// <param name="vm"></param>
        [HttpPut("EditRole")]
        public async Task<JsonResponse> EditApiResource([FromBody] EditApiResourceViewModel vm)
        {
            var apiResource = (await _apiResourceServices.Query(p => p.Id == vm.Id)).SingleOrDefault();
            if (apiResource == null)
            {
                throw new UserOperationException("接口不存在");
            }
            apiResource = _mapper.Map<SysApiResource>(vm);

            var uResult = await _apiResourceServices.Update(apiResource);
            if (uResult <= 0)
            {
                throw new UserOperationException("更新失败");
            }
            return new JsonResponse(true);
        }
        /// <summary>
        /// 删除接口
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete("DeleteRole")]
        public async Task<JsonResponse> DeleteApiResource(int id)
        {
            var apiResource = (await _apiResourceServices.Query(p => p.Id == id)).SingleOrDefault();
            if (apiResource == null)
            {
                throw new UserOperationException("接口不存在");
            }
            var dResult = await _apiResourceServices.DeleteByIdAsync(id);
            if (dResult <= 0)
            {
                throw new UserOperationException("删除失败");
            }
            return new JsonResponse(true);
        }
    }
}