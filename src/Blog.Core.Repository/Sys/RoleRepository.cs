using Blog.Core.IRepository;
using Blog.Core.Model.Dtos.Role;
using Blog.Core.Model.Models;
using Blog.Core.Repository.Base;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Repository
{
    public class RoleRepository : BaseRepository<SysRole>, IRoleRepository
    {
        public RoleRepository(ISqlSugarClient db)
            : base(db)
        {

        }
        public async Task<ISugarQueryable<RoleDto>> GetRoles()
        {
            var roles = _db.Queryable<SysUser, SysUserRole, SysRole>((u, ur, r) => u.Id == ur.UserId && ur.RoleId == r.Id).Select((u, ur, r) => new RoleDto
            {
                Id = r.Id,
                Name = r.Name,
                Enabled = r.Enabled,
                Remark = r.Remark,
                CreateTime = r.CreateTime,
                Creator = r.Creator,
                ModifyTime = r.ModifyTime,
                UserId = u.Id
            });

            return await Task.Run(() => roles);
        }
        public async Task<ISugarQueryable<SysRole>> GetRoleByUserId(int userId)
        {
            var roles = _db.Queryable<SysUser, SysUserRole, SysRole>((u, ur, r) => u.Id == ur.UserId && ur.RoleId == r.Id).Select((u, ur, r) => new SysRole
            {
                Id = r.Id,
                Name = r.Name,
                Enabled = r.Enabled,
                Remark = r.Remark,
                CreateTime = r.CreateTime,
                Creator = r.Creator,
                ModifyTime = r.ModifyTime
            });

            return await Task.Run(() => roles);
        }

    }
}
