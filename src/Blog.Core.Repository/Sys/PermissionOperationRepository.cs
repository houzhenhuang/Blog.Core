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
    public class PermissionOperationRepository : BaseRepository<SysPermissionOperation>, IPermissionOperationRepository
    {
        public PermissionOperationRepository(ISqlSugarClient db)
            : base(db)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<ISugarQueryable<PermissionDto>> GetPermissionOperations()
        {
            var permissionMenus = _db.Queryable<SysPermission, SysPermissionOperation, SysOperation>(
                (p, po, o) => new object[] {
                    JoinType.Inner,p.Id==po.PermissionId,
                    JoinType.Inner,po.OperationId==o.Id
                })
                .Where((p, po, o) => p.Type == SysConst.OPERATION)
                .Select((p, po, o) => new PermissionDto
                {
                    Id = p.Id,
                    MenuId = o.MenuId,
                    Name = o.Name,
                    ParentId = o.ParentId,
                    Type = p.Type
                });
            return await Task.Run(() => permissionMenus);
        }
    }
}
