using Blog.Core.IRepository;
using Blog.Core.Model.Dtos;
using Blog.Core.Model.Dtos.Permission;
using Blog.Core.Model.Enums;
using Blog.Core.Model.Models;
using Blog.Core.Repository.Base;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Repository
{
    public class PermissionRepository : BaseRepository<SysPermission>, IPermissionRepository
    {
        public PermissionRepository(ISqlSugarClient db)
            : base(db)
        {

        }
        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ISugarQueryable<UserPermissionDto>> GetUserPermissions(int userId)
        {
            var list = _db.Queryable<SysUser, SysUserRole, SysRole, SysRolePermission, SysPermission, SysPermissionOperation, SysOperation, SysApiResource>(
                            (u, ur, r, rp, p, po, o, ar) => new object[] {
                                JoinType.Inner,u.Id==ur.UserId,
                                JoinType.Inner,ur.RoleId==r.Id,
                                JoinType.Inner,r.Id==rp.RoleId,
                                JoinType.Inner,rp.PermissionId==p.Id,
                                JoinType.Inner,p.Id==po.PermissionId,
                                JoinType.Inner,po.OperationId==o.Id,
                                JoinType.Left,o.ApiResourceId==ar.Id
                            })
                            .Where((u, ur, r, rp, p, po, o, ar) => u.Id == userId)
                            .Select((u, ur, r, rp, p, po, o, ar) => new UserPermissionDto
                            {
                                RoleId = r.Id,
                                RoleName = r.Name,
                                LinkUrl = ar.LinkUrl
                            });
            return await Task.Run(() => list);
        }
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        public async Task<ISugarQueryable<MenuDto>> GetMenus()
        {
            var menus = _db.Queryable<SysMenu>()
                .OrderBy(m => m.Sort)
                .Select(m => new MenuDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    ParentId = m.ParentId,
                    Route = m.Route,
                    Icon = m.Icon,
                    Enabled = m.Enabled,
                    Sort = m.Sort,
                    CreateTime = m.CreateTime,
                    Remark = m.Remark
                });
            return await Task.Run(() => menus);
        }
    }
}
