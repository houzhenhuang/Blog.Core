using Blog.Core.IRepository.UnitOfWork;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Core.Repository.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ISqlSugarClient _db;
        public UnitOfWork(ISqlSugarClient db)
        {
            this._db = db;
        }
        public void Begin()
        {
            _db.Ado.BeginTran();
        }
        public void Commit()
        {
            _db.Ado.CommitTran();
        }
        public void Rollback()
        {
            _db.Ado.RollbackTran();
        }
        public ISqlSugarClient GetSqlSugarClient()
        {
            return _db;
        }
    }
}
