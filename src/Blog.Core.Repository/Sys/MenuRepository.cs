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
    public class MenuRepository : BaseRepository<SysMenu>, IMenuRepository
    {
        public MenuRepository(ISqlSugarClient db)
            : base(db)
        {

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
