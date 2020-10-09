using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.IRepository.UnitOfWork
{
    public interface IUnitOfWork
    {
        void Begin();
        void Commit();
        void Rollback();
        ISqlSugarClient GetSqlSugarClient();
    }
}
