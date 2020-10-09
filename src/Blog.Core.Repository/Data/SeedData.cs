using Blog.Core.Common.Helper;
using Blog.Core.IRepository.Data;
using Blog.Core.Model;
using Blog.Core.Model.Consts;
using Blog.Core.Model.Core;
using Blog.Core.Model.Enums;
using Blog.Core.Model.Models;
using Blog.Core.Repository.Sugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Blog.Core.Repository.Data
{
    public class SeedData : ISeedData
    {
        private readonly ISqlSugarClient _db;
        public SeedData(ISqlSugarClient db)
        {
            _db = db;
        }
        public async Task SeedAsync()
        {
            var context = new DbContext(_db);

            context.Db.DbMaintenance.CreateDatabase();

            context.CreateTableByEntity<Advertisement>(false, new Advertisement());
            var entity = context.GetEntityDB<Advertisement>(context.Db);
            entity.InsertRange(new List<Advertisement>
            {
                new Advertisement { Id = 1, Title = "测试一波", Url = "广告链接", ImgUrl = "广告图片", Remark = "广告备注", CreateTime = DateTime.Now },
                new Advertisement { Id = 2, Title = "测试二波", Url = "广告链接", ImgUrl = "广告图片", Remark = "广告备注", CreateTime = DateTime.Now },
                new Advertisement { Id = 3, Title = "测试三波", Url = "广告链接", ImgUrl = "广告图片", Remark = "广告备注", CreateTime = DateTime.Now },
                new Advertisement { Id = 4, Title = "测试四波", Url = "广告链接", ImgUrl = "广告图片", Remark = "广告备注", CreateTime = DateTime.Now },
            });

            context.CreateTableByEntity<BlogArticle>(false, new BlogArticle());
            var blogArticle = context.GetEntityDB<BlogArticle>(context.Db);
            blogArticle.InsertRange(new List<BlogArticle>
            {
                new BlogArticle { Id = 1, Submitter="admin", Title = "技术博文测试一波", Category="技术博文",Content="博文内容1", Remark = "博文1", CreateTime = DateTime.Now },
                new BlogArticle { Id = 2, Submitter="admin", Title = "技术博文测试二波", Category="技术博文",Content="博文内容2", Remark = "博文2", CreateTime = DateTime.Now },
                new BlogArticle { Id = 3, Submitter="admin", Title = "技术博文测试三波", Category="技术博文",Content="博文内容3", Remark = "博文3", CreateTime = DateTime.Now },
                new BlogArticle { Id = 4, Submitter="admin", Title = "技术博文测试四波", Category="技术博文",Content="博文内容4", Remark = "博文4", CreateTime = DateTime.Now }
            });


            var pType = typeof(Entity);
            var modelTypes = Assembly.GetAssembly(pType).GetTypes().Where(t => t.IsClass == true && t.IsSubclassOf(pType)).ToList();

            foreach (var modelType in modelTypes)
            {
                if (!context.Db.DbMaintenance.IsAnyTable(modelType.Name) && modelType.Name != nameof(BaseEntity))
                {
                    context.Db.CodeFirst.InitTables(modelType);
                }
            }
            if (!await context.Db.Queryable<SysUser>().AnyAsync())
            {
                context.GetEntityDB<SysUser>().InsertRange(new List<SysUser>
                {
                    new SysUser { Id = 1, UserName = "admin", Password = MD5Helper.MD5Encrypt32("123456"), RealName = "管理员", Avatar = "https://wpimg.wallstcn.com/f778738c-e4f8-4870-b634-56703b4acafe.gif", Status = 1 },
                });
            }
            if (!await context.Db.Queryable<SysRole>().AnyAsync())
            {
                context.GetEntityDB<SysRole>().InsertRange(new List<SysRole>
                {
                    new SysRole { Id = 1, Name = "超级管理员", Enabled = true, Remark = "这个角色拥有全部权限" }
                });
            }
            if (!await context.Db.Queryable<SysUserRole>().AnyAsync())
            {
                context.GetEntityDB<SysUserRole>().InsertRange(new List<SysUserRole>
                {
                    new SysUserRole { UserId = 1, RoleId = 1 }
                });
            }

            if (!await context.Db.Queryable<SysApiResource>().AnyAsync())
            {
                context.GetEntityDB<SysApiResource>().InsertRange(new List<SysApiResource>
                {
                    new SysApiResource { Id = 1, Name = "分页获取用户列表", Code = "", LinkUrl = "/api/user/getusersbypage", Sort = 1, Creator = 1 },
                    new SysApiResource { Id = 2, Name = "分页获取角色列表", Code = "", LinkUrl = "/api/role/getrolesbypage", Sort = 2, Creator = 1 },
                    new SysApiResource { Id = 3, Name = "分页获取api接口列表", Code = "", LinkUrl = "/api/apiresource/getapiresourcesbypage", Sort = 3, Creator = 1 },
                    new SysApiResource { Id = 4, Name = "获取全部菜单列表", Code = "", LinkUrl = "/api/menu/getmenus", Sort = 4, Creator = 1 },
                    new SysApiResource { Id = 5, Name = "获取全部角色列表", Code = "", LinkUrl = "/api/role/getroles", Sort = 5, Creator = 1 },
                    new SysApiResource { Id = 6, Name = "获取全部权限列表", Code = "", LinkUrl = "/api/permission/getpermissions", Sort = 6, Creator = 1 },
                    new SysApiResource { Id = 7, Name = "获取角色权限列表", Code = "", LinkUrl = "/api/rolepermission/getrolepermissions", Sort = 7, Creator = 1 },
                    new SysApiResource { Id = 8, Name = "获取全部api接口列表", Code = "", LinkUrl = "/api/apiresource/getapiresources", Sort = 8, Creator = 1 },
                    new SysApiResource { Id = 9, Name = "获取菜单操作功能", Code = "", LinkUrl = "/api/operation/getoperationsbymenuid", Sort = 9, Creator = 1 },

                    new SysApiResource { Id = 10, Name = "添加用户", Code = "", LinkUrl = "/api/user/adduser", Sort = 9, Creator = 1 },
                    new SysApiResource { Id = 11, Name = "编辑用户", Code = "", LinkUrl = "/api/user/edituser", Sort = 10, Creator = 1 },
                    new SysApiResource { Id = 12, Name = "删除用户", Code = "", LinkUrl = "/api/user/deleteuser", Sort = 11, Creator = 1 },
                    new SysApiResource { Id = 13, Name = "根据用户获取角色", Code = "", LinkUrl = "/api/userrole/getrolesbyuser", Sort = 12, Creator = 1 },
                    new SysApiResource { Id = 14, Name = "为用户分配角色", Code = "", LinkUrl = "/api/userrole/disrole", Sort = 13, Creator = 1 },

                    new SysApiResource { Id = 15, Name = "添加角色", Code = "", LinkUrl = "/api/role/addrole", Sort = 14, Creator = 1 },
                    new SysApiResource { Id = 16, Name = "编辑角色", Code = "", LinkUrl = "/api/role/editrole", Sort = 15, Creator = 1 },
                    new SysApiResource { Id = 17, Name = "删除角色", Code = "", LinkUrl = "/api/role/deleterole", Sort = 16, Creator = 1 },

                    new SysApiResource { Id = 18, Name = "添加接口", Code = "", LinkUrl = "/api/apiresource/addapiresource", Sort = 17, Creator = 1 },
                    new SysApiResource { Id = 19, Name = "编辑接口", Code = "", LinkUrl = "/api/apiresource/editapiresource", Sort = 18, Creator = 1 },
                    new SysApiResource { Id = 20, Name = "删除接口", Code = "", LinkUrl = "/api/apiresource/deleteapiresource", Sort = 19, Creator = 1 },

                    new SysApiResource { Id = 21, Name = "添加菜单", Code = "", LinkUrl = "/api/permission/addmenu", Sort = 20, Creator = 1 },
                    new SysApiResource { Id = 22, Name = "编辑菜单", Code = "", LinkUrl = "/api/permission/editmenu", Sort = 21, Creator = 1 },
                    new SysApiResource { Id = 23, Name = "删除菜单", Code = "", LinkUrl = "/api/permission/deletemenu", Sort = 22, Creator = 1 },

                    new SysApiResource { Id = 24, Name = "保存角色权限", Code = "", LinkUrl = "/api/rolepermission/saverolepermission", Sort = 23, Creator = 1 },

                    new SysApiResource { Id = 25, Name = "添加菜单功能", Code = "", LinkUrl = "/api/operation/addoperation", Sort = 20, Creator = 1 },
                    new SysApiResource { Id = 26, Name = "编辑菜单功能", Code = "", LinkUrl = "/api/operation/editoperation", Sort = 21, Creator = 1 },
                    new SysApiResource { Id = 27, Name = "删除菜单功能", Code = "", LinkUrl = "/api/operation/deleteoperation", Sort = 22, Creator = 1 },
                });
            }

            if (!await context.Db.Queryable<SysMenu>().AnyAsync())
            {
                context.GetEntityDB<SysMenu>().InsertRange(new List<SysMenu>
                {
                   new SysMenu { Id = 1, Name = "演示", ParentId = 0, Route = "/example", Icon = "example", Enabled = true, Sort = 1, Creator = 1, Reviser = 1 },

                   new SysMenu { Id = 2, Name = "博客管理", ParentId = 0, Route = "/blog", Icon = "blog", Enabled = true, Sort = 2, Creator = 1 },
                   new SysMenu { Id = 3, Name = "基本设置", ParentId = 0, Route = "/basic", Icon = "setting", Enabled = true, Sort = 3, Creator = 1 },
                   new SysMenu { Id = 4, Name = "系统管理", ParentId = 0, Route = "/system", Icon = "system", Enabled = true, Sort = 5, Creator = 1 },
                   new SysMenu { Id = 5, Name = "多级路由", ParentId = 0, Route = "/nested", Icon = "nested", Enabled = true, Sort = 6, Creator = 1 },

                   new SysMenu { Id = 6, Name = "博客管理", ParentId = 2, Route = "/blog/list", Icon = "blog", Enabled = true, Sort = 1, Creator = 1 },

                   new SysMenu { Id = 7, Name = "用户管理", ParentId = 3, Route = "/basic/user", Icon = "user", Enabled = true, Sort = 1, Creator = 1 },
                   new SysMenu { Id = 8, Name = "角色管理", ParentId = 3, Route = "/basic/role", Icon = "role", Enabled = true, Sort = 2, Creator = 1 },
                   new SysMenu { Id = 9, Name = "接口管理", ParentId = 3, Route = "/basic/api", Icon = "api", Enabled = true, Sort = 3, Creator = 1 },
                   new SysMenu { Id = 10, Name = "菜单管理", ParentId = 3, Route = "/basic/menu", Icon = "menu", Enabled = true, Sort = 4, Creator = 1 },
                   new SysMenu { Id = 11, Name = "功能操作", ParentId = 3, Route = "/basic/operation", Icon = "operation", Enabled = true, Sort = 5, Creator = 1 },
                   new SysMenu { Id = 12, Name = "权限分配", ParentId = 3, Route = "/basic/permission", Icon = "permission", Enabled = true, Sort = 6, Creator = 1 },

                   new SysMenu { Id = 13, Name = "个人中心", ParentId = 4, Route = "/system/person-center", Icon = "personalCenter", Enabled = true, Sort = 1, Creator = 1 },

                   new SysMenu { Id = 14, Name = "Menu-1", ParentId = 5, Route = "/nested/menu1", Icon = "menu", Enabled = true, Sort = 1, Creator = 1 },
                   new SysMenu { Id = 15, Name = "Menu-2", ParentId = 5, Route = "/nested/menu2", Icon = "menu", Enabled = true, Sort = 2, Creator = 1 },

                   new SysMenu { Id = 16, Name = "Menu-1-1", ParentId = 14, Route = "/nested/menu1/menu1-1", Icon = "menu", Enabled = true, Sort = 1, Creator = 1 },
                   new SysMenu { Id = 17, Name = "Menu-1-2", ParentId = 14, Route = "/nested/menu1/menu1-2", Icon = "menu", Enabled = true, Sort = 2, Creator = 1 },
                   new SysMenu { Id = 18, Name = "Menu-1-3", ParentId = 14, Route = "/nested/menu1/menu1-3", Icon = "menu", Enabled = true, Sort = 2, Creator = 1 },

                   new SysMenu { Id = 19, Name = "Menu-1-2-1", ParentId = 17, Route = "/nested/menu1/menu1-2/menu1-2-1", Icon = "menu", Enabled = true, Sort = 1, Creator = 1 },
                   new SysMenu { Id = 20, Name = "Menu-1-2-2", ParentId = 17, Route = "/nested/menu1/menu1-2/menu1-2-2", Icon = "menu", Enabled = true, Sort = 1, Creator = 1 },

                   new SysMenu { Id = 21, Name = "Table", ParentId = 1, Route = "/example/table", Icon = "table", Enabled = true, Sort = 1, Creator = 1 },
                   new SysMenu { Id = 22, Name = "Tree", ParentId = 1, Route = "/example/tree", Icon = "tree", Enabled = true, Sort = 2, Creator = 1 },
                });
            }

            if (!await context.Db.Queryable<SysOperation>().AnyAsync())
            {
                context.GetEntityDB<SysOperation>().InsertRange(new List<SysOperation>
                {
                    new SysOperation { Id = 1, Name = "分页获取用户列表", Code = "getusersbypage", ApiResourceId = 1, ParentId = 0 , Sort = 1,MenuId = 7},
                    new SysOperation { Id = 2, Name = "新增用户", Code = "adduser", ApiResourceId = 10, ParentId = 0 , Sort = 1,MenuId = 7},
                    new SysOperation { Id = 3, Name = "编辑用户", Code = "getusersbypage", ApiResourceId = 11, ParentId = 0 , Sort = 1,MenuId = 7},
                    new SysOperation { Id = 4, Name = "删除用户", Code = "getusersbypage", ApiResourceId = 12, ParentId = 0 , Sort = 1,MenuId = 7},
                    new SysOperation { Id = 5, Name = "获取全部角色列表", Code = "getroles", ApiResourceId = 5, ParentId = 0 , Sort = 1,MenuId = 7},
                    new SysOperation { Id = 6, Name = "根据用户获取角色", Code = "getrolesbyuser", ApiResourceId = 13, ParentId = 0 , Sort = 1,MenuId = 7},
                    new SysOperation { Id = 7, Name = "为用户分配角色", Code = "disrole", ApiResourceId = 14, ParentId = 0 , Sort = 1,MenuId = 7},

                    new SysOperation { Id = 8, Name = "分页获取角色列表", Code = "getrolesbypage", ApiResourceId = 2, ParentId = 0 , Sort = 1,MenuId = 8},
                    new SysOperation { Id = 9, Name = "添加角色", Code = "addrole", ApiResourceId = 15, ParentId = 0 , Sort = 1,MenuId = 8},
                    new SysOperation { Id = 10, Name = "编辑角色", Code = "editrole", ApiResourceId = 16, ParentId = 0 , Sort = 1,MenuId = 8},
                    new SysOperation { Id = 11, Name = "删除角色", Code = "deleterole", ApiResourceId = 17, ParentId = 0 , Sort = 1,MenuId = 8},

                    new SysOperation { Id = 12, Name = "分页获取api接口列表", Code = "getapiresourcesbypage", ApiResourceId = 3, ParentId = 0 , Sort = 1,MenuId = 9},
                    new SysOperation { Id = 13, Name = "添加接口", Code = "addrole", ApiResourceId = 18, ParentId = 0 , Sort = 1,MenuId = 9},
                    new SysOperation { Id = 14, Name = "编辑接口", Code = "editrole", ApiResourceId = 19, ParentId = 0 , Sort = 1,MenuId = 9},
                    new SysOperation { Id = 15, Name = "删除接口", Code = "deleterole", ApiResourceId = 20, ParentId = 0 , Sort = 1,MenuId = 9},

                    new SysOperation { Id = 16, Name = "获取全部菜单列表", Code = "getmenus", ApiResourceId = 4, ParentId = 0 , Sort = 1,MenuId = 10},
                    new SysOperation { Id = 17, Name = "添加菜单", Code = "addmenu", ApiResourceId = 21, ParentId = 0 , Sort = 1,MenuId = 10},
                    new SysOperation { Id = 18, Name = "编辑菜单", Code = "editmenu", ApiResourceId = 22, ParentId = 0 , Sort = 1,MenuId = 10},
                    new SysOperation { Id = 19, Name = "删除菜单", Code = "deletemenu", ApiResourceId = 23, ParentId = 0 , Sort = 1,MenuId = 10},

                    new SysOperation { Id = 20, Name = "获取全部菜单列表", Code = "getmenus", ApiResourceId = 4, ParentId = 0 , Sort = 1,MenuId = 11},
                    new SysOperation { Id = 21, Name = "获取菜单操作功能", Code = "getoperationsbymenuid", ApiResourceId = 9, ParentId = 0 , Sort = 1,MenuId = 11},
                    new SysOperation { Id = 22, Name = "添加菜单功能", Code = "addoperation", ApiResourceId = 25, ParentId = 0 , Sort = 1,MenuId = 11},
                    new SysOperation { Id = 23, Name = "编辑菜单功能", Code = "editoperation", ApiResourceId = 26, ParentId = 0 , Sort = 1,MenuId = 11},
                    new SysOperation { Id = 24, Name = "删除菜单功能", Code = "deleteoperation", ApiResourceId = 27, ParentId = 0 , Sort = 1,MenuId = 11},
                    new SysOperation { Id = 25, Name = "获取全部api接口列表", Code = "getapiresources", ApiResourceId = 8, ParentId = 0 , Sort = 1,MenuId = 11},

                    new SysOperation { Id = 26, Name = "获取全部角色列表", Code = "getroles", ApiResourceId = 5, ParentId = 0 , Sort = 1,MenuId = 12},
                    new SysOperation { Id = 27, Name = "获取全部权限列表", Code = "getpermissions", ApiResourceId = 6, ParentId = 0 , Sort = 1,MenuId = 12},
                    new SysOperation { Id = 28, Name = "获取角色权限列表", Code = "getrolepermissions", ApiResourceId = 7, ParentId = 0 , Sort = 1,MenuId = 12},
                    new SysOperation { Id = 29, Name = "保存角色权限", Code = "saverolepermission", ApiResourceId = 24, ParentId = 0 , Sort = 1,MenuId = 12},

                });
            }
            if (!await context.Db.Queryable<SysPermission>().AnyAsync())
            {
                var menus = context.Db.Queryable<SysMenu>().ToList();
                var permissionMenus = new List<SysPermission>();
                foreach (var menu in menus)
                {
                    permissionMenus.Add(new SysPermission { Id = menu.Id, Name = menu.Name, Type = SysConst.MENU, Creator = 1 });
                }
                context.GetEntityDB<SysPermission>().InsertRange(permissionMenus);

                var maxId = permissionMenus.Max(m => m.Id);

                var operations = context.Db.Queryable<SysOperation>().ToList();
                var permissionOperations = new List<SysPermission>();
                foreach (var operation in operations)
                {
                    permissionOperations.Add(new SysPermission { Id = operation.Id + maxId, Name = operation.Name, Type = SysConst.OPERATION, Creator = 1 });
                }
                context.GetEntityDB<SysPermission>().InsertRange(permissionOperations);
            }

            if (!await context.Db.Queryable<SysRolePermission>().AnyAsync())
            {
                var permissions = context.Db.Queryable<SysPermission>().ToList();
                var rolePermissions = new List<SysRolePermission>();
                foreach (var permission in permissions)
                {
                    rolePermissions.Add(new SysRolePermission { RoleId = 1, PermissionId = permission.Id });
                }
                context.GetEntityDB<SysRolePermission>().InsertRange(rolePermissions);
            }

            if (!await context.Db.Queryable<SysPermissionMenu>().AnyAsync())
            {
                var permissions = context.Db.Queryable<SysPermission>().Where(p => p.Type == SysConst.MENU).ToList();
                var permissionMenus = new List<SysPermissionMenu>();
                foreach (var permission in permissions)
                {
                    permissionMenus.Add(new SysPermissionMenu { PermissionId = permission.Id, MenuId = permission.Id });
                }
                context.GetEntityDB<SysPermissionMenu>().InsertRange(permissionMenus);
            }

            if (!await context.Db.Queryable<SysPermissionOperation>().AnyAsync())
            {
                var maxId = context.Db.Queryable<SysPermission>().Where(p => p.Type == SysConst.MENU).Max(p => p.Id);
                var permissions = context.Db.Queryable<SysPermission>().Where(p => p.Type == SysConst.OPERATION).ToList();
                var permissionMenus = new List<SysPermissionOperation>();
                foreach (var permission in permissions)
                {
                    permissionMenus.Add(new SysPermissionOperation { PermissionId = permission.Id, OperationId = permission.Id - maxId });
                }
                context.GetEntityDB<SysPermissionOperation>().InsertRange(permissionMenus);
            }
        }
    }
}
