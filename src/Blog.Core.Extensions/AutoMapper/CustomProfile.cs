using AutoMapper;
using Blog.Core.Model.Dtos.ApiResource;
using Blog.Core.Model.Dtos.Operation;
using Blog.Core.Model.Dtos.Permission;
using Blog.Core.Model.Dtos.Role;
using Blog.Core.Model.Dtos.User;
using Blog.Core.Model.Models;
using Blog.Core.Model.ViewModels;
using Blog.Core.Model.ViewModels.Permission;
using Blog.Core.Model.ViewModels.Role;
using Blog.Core.Model.ViewModels.User;

namespace Blog.Core.Extensions.AutoMapper
{
    public class CustomProfile : Profile
    {
        /// <summary>
        /// 配置构造函数，用来创建关系映射
        /// 
        /// Profile不知有什么用，通过百度了解才了解是services.AddAutoMapper是会自动找到所有继承了Profile的类然后进行配置，
        /// 而且我的这个配置文件是在api层的，如果Profile配置类放在别的层（比如Service层），
        /// 如果没解耦的话，可以services.AddAutoMapper()，参数留空，
        /// AutoMapper会从所有引用的程序集里找继承Profile的类，如果解耦了，就得services.AddAutoMapper(Assembly.Load("Blog.Core.Service"))。
        /// </summary>
        public CustomProfile()
        {
            CreateMap<BlogArticle, BlogViewModel>();

            CreateMap<SysUser, UserDto>();
            CreateMap<AddUserViewModel, SysUser>();
            CreateMap<EditUserViewModel, SysUser>();

            CreateMap<SysRole, RoleDto>();
            CreateMap<AddRoleViewModel, SysRole>();
            CreateMap<EditRoleViewModel, SysRole>();

            CreateMap<SysPermission, PermissionDto>();
            CreateMap<AddMenuViewModel, SysPermission>();
            CreateMap<EditMenuViewModel, SysPermission>();

            CreateMap<SysApiResource, ApiResourceDto>();

            CreateMap<AddOperationDto, SysOperation>();
            CreateMap<EditOperationDto, SysOperation>();


        }
    }
}
