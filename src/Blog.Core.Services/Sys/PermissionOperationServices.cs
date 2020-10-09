using AutoMapper;
using Blog.Core.Common.Attributes;
using Blog.Core.IRepository;
using Blog.Core.IRepository.Base;
using Blog.Core.IServices;
using Blog.Core.Model.Consts;
using Blog.Core.Model.Dtos;
using Blog.Core.Model.Dtos.Permission;
using Blog.Core.Model.Enums;
using Blog.Core.Model.Models;
using Blog.Core.Model.ViewModels;
using Blog.Core.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Blog.Core.Services
{
    public class PermissionOperationServices : BaseServices<SysPermissionOperation>, IPermissionOperationServices
    {
        private readonly IPermissionOperationRepository _permissionOperationRepository;
        public PermissionOperationServices(IPermissionOperationRepository permissionOperationRepository,
            IBaseRepository<SysPermissionOperation> baseRepository)
            : base(baseRepository)
        {
            _permissionOperationRepository = permissionOperationRepository;
        }
    }
}
