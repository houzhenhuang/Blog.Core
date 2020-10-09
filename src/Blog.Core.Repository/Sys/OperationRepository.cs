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
    public class OperationRepository : BaseRepository<SysOperation>, IOperationRepository
    {
        public OperationRepository(ISqlSugarClient db)
            : base(db)
        {

        }
    }
}
