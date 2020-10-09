using Blog.Core.IRepository;
using Blog.Core.Model.Consts;
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
    public class PermissionMenuRepository : BaseRepository<SysPermissionMenu>, IPermissionMenuRepository
    {
        public PermissionMenuRepository(ISqlSugarClient db)
            : base(db)
        {

        }
        /// <summary>
        /// 获取权限菜单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ISugarQueryable<PermissionMenuDto>> GetPermissionMenus(int userId)
        {
            var userPermisMenus = _db.Queryable<SysUser, SysUserRole, SysRole, SysRolePermission, SysPermission, SysPermissionMenu, SysMenu>(
                (u, ur, r, rp, p, pm, m) => new object[] {
                    JoinType.Inner,u.Id==ur.UserId,
                    JoinType.Inner,ur.RoleId==r.Id,
                    JoinType.Inner,r.Id==rp.RoleId,
                    JoinType.Inner,rp.PermissionId==p.Id,
                    JoinType.Inner,p.Id==pm.PermissionId,
                    JoinType.Inner,pm.MenuId==m.Id
                })
                .Where((u, ur, r, rp, p, pm, m) => u.Id == userId && p.Type == SysConst.MENU)
                .Select((u, ur, r, rp, p, pm, m) => new PermissionMenuDto
                {
                    Id = m.Id,
                    Name = m.Name,
                    ParentId = m.ParentId,
                    Sort = m.Sort,
                    Path = m.Route,
                    Icon = m.Icon
                });
            return await Task.Run(() => userPermisMenus);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<ISugarQueryable<PermissionDto>> GetPermissionMenus()
        {
            var permissionMenus = _db.Queryable<SysPermission, SysPermissionMenu, SysMenu>(
                (p, pm, m) => new object[] {
                    JoinType.Inner,p.Id==pm.PermissionId,
                    JoinType.Inner,pm.MenuId==m.Id
                })
                .Where((p, pm, m) => p.Type == SysConst.MENU)
                .Select((p, pm, m) => new PermissionDto
                {
                    Id = p.Id,
                    MenuId = m.Id,
                    Name = m.Name,
                    ParentId = m.ParentId,
                    Type = p.Type
                });
            return await Task.Run(() => permissionMenus);
        }
    }
}
