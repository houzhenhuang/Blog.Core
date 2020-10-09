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
    public class ApiResourceRepository : BaseRepository<SysApiResource>, IApiResourceRepository
    {
        public ApiResourceRepository(ISqlSugarClient db)
            : base(db)
        {

        }
    }
}
