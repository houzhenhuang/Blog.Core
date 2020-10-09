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
    public class UserServices : BaseServices<SysUser>, IUserServices
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserServices(IUserRepository userRepository, IBaseRepository<SysUser> baseRepository, IMapper mapper)
            : base(baseRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
    }
}
