using Blog.Core.IRepository;
using Blog.Core.Model.Models;
using Blog.Core.Repository.Base;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Repository
{
    public class UserRoleRepository : BaseRepository<SysUserRole>, IUserRoleRepository
    {
        public UserRoleRepository(ISqlSugarClient db)
            : base(db)
        {
        }
        public async Task<ISugarQueryable<SysRole>> GetRoleByUserId(int userId)
        {
            var roles = _db.Queryable<SysRole, SysUserRole, SysUser>((r, ur, u) => new object[] {
                JoinType.Inner,r.Id==ur.RoleId,
                JoinType.Inner,u.Id==ur.UserId
            })
            .Where((r, ur, u) => u.Id == userId && r.Enabled == true)
            .Select((r, ur, u) => r);
            return await Task.Run(() => roles);
        }
    }
}
