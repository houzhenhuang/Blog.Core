using Blog.Core.IRepository;
using Blog.Core.Model.Consts;
using Blog.Core.Model.Dtos;
using Blog.Core.Model.Dtos.Permission;
using Blog.Core.Model.Dtos.RolePermission;
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
    public class RolePermissionRepository : BaseRepository<SysRolePermission>, IRolePermissionRepository
    {
        public RolePermissionRepository(ISqlSugarClient db)
            : base(db)
        {

        }
        /// <summary>
        /// 根据角色获取权限id列表
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<ISugarQueryable<int>> GetPermissionIds(int roleId)
        {
            var permissionIds = _db.Queryable<SysRole, SysRolePermission, SysPermission>((r, rp, p) => new object[]
            {
                JoinType.Inner,r.Id==rp.RoleId,
                JoinType.Inner,rp.PermissionId==p.Id
            }).Where((r, rp, p) => r.Id == roleId).Select((r, rp, p) => p.Id);

            return await Task.Run(() => permissionIds);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public async Task<ISugarQueryable<PermissionDto>> GetPermissionByRole(int roleId, string permissionType)
        {
            ISugarQueryable<PermissionDto> permissions = null;
            if (permissionType == SysConst.MENU)
            {
                permissions = _db.Queryable<SysRole, SysRolePermission, SysPermission, SysPermissionMenu, SysMenu>((r, rp, p, pm, m) => new object[]
                {
                    JoinType.Inner,r.Id==rp.RoleId,
                    JoinType.Inner,rp.PermissionId==p.Id,
                    JoinType.Inner,p.Id==pm.PermissionId,
                    JoinType.Inner,pm.MenuId==m.Id
                }).Where((r, rp, p, pm, m) => r.Id == roleId && p.Type == permissionType).Select((r, rp, p, pm, m) => new PermissionDto
                {
                    Id = p.Id,
                    ParentId = m.ParentId
                });
            }
            else if (permissionType == SysConst.OPERATION)
            {
                permissions = _db.Queryable<SysRole, SysRolePermission, SysPermission, SysPermissionOperation, SysOperation>((r, rp, p, po, o) => new object[]
                {
                    JoinType.Inner,r.Id==rp.RoleId,
                    JoinType.Inner,rp.PermissionId==p.Id,
                    JoinType.Inner,p.Id==po.PermissionId,
                    JoinType.Inner,po.OperationId==o.Id
                }).Where((r, rp, p, po, o) => r.Id == roleId && p.Type == permissionType).Select((r, rp, p, po, o) => new PermissionDto
                {
                    Id = p.Id,
                    ParentId = o.ParentId
                });
            }
            return await Task.Run(() => permissions);
        }
    }
}
