using AutoMapper;
using Blog.Core.Common.Attributes;
using Blog.Core.IRepository;
using Blog.Core.IRepository.Base;
using Blog.Core.IServices;
using Blog.Core.Model.Consts;
using Blog.Core.Model.Dtos;
using Blog.Core.Model.Dtos.Operation;
using Blog.Core.Model.Dtos.Permission;
using Blog.Core.Model.Enums;
using Blog.Core.Model.Models;
using Blog.Core.Model.ViewModels;
using Blog.Core.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Blog.Core.Services
{
    public class OperationServices : BaseServices<SysOperation>, IOperationServices
    {
        private readonly IOperationRepository _operationRepository;
        private readonly IMapper _mapper;
        public OperationServices(IOperationRepository operationRepository,
            IBaseRepository<SysOperation> baseRepository, IMapper mapper)
            : base(baseRepository)
        {
            _operationRepository = operationRepository;
            _mapper = mapper;
        }

        public async Task<List<OperationDto>> GetOperationsByMenuId(int menuId)
        {
            var operations = await _operationRepository.Query(o => o.MenuId == menuId);

            return operations.Select(o => new OperationDto
            {
                Id = o.Id,
                Name = o.Name,
                Code = o.Code,
                ApiResourceId = o.ApiResourceId,
                MenuId = o.MenuId,
                Sort = o.Sort
            }).ToList();
        }
    }
}
