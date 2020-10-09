using AutoMapper;
using Blog.Core.Common.Attributes;
using Blog.Core.IRepository;
using Blog.Core.IRepository.Base;
using Blog.Core.IServices;
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
    public class UserRoleServices : BaseServices<SysUserRole>, IUserRoleServices
    {
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IMapper _mapper;
        public UserRoleServices(IUserRoleRepository userRoleRepository, IBaseRepository<SysUserRole> baseRepository, IMapper mapper)
            : base(baseRepository)
        {
            _userRoleRepository = userRoleRepository;
            _mapper = mapper;
        }
        public async Task<List<SysRole>> GetRoleByUserId(int userId)
        {
            var roles = await _userRoleRepository.GetRoleByUserId(userId);
            return roles.ToList();
        }
    }
}
