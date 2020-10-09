using Blog.Core.Model;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.IRepository.Base
{
    public interface IBaseRepository<TEntity> where TEntity : class, new()
    {
        Task<TEntity> QueryById(object objId);
        Task<TEntity> QueryById(object objId, bool blnUseCache = false);
        Task<ISugarQueryable<TEntity>> QueryByIds(object[] lstIds);

        int Add(TEntity model);
        Task<int> AddAsync(TEntity model);

        int DeleteById(object id);
        Task<int> DeleteByIdAsync(object id);
        Task<int> Delete(TEntity model);
        Task<int> DeleteByIds(object[] ids);
        Task<int> Update(TEntity model);
        Task<int> Update(TEntity entity, string strWhere);
        Task<int> Delete(Expression<Func<TEntity, bool>> whereExpression);
        Task<int> Update(TEntity entity, List<string> lstColumns = null, List<string> lstIgnoreColumns = null, string strWhere = "");

        Task<ISugarQueryable<TEntity>> Query();
        Task<ISugarQueryable<TEntity>> Query(string strWhere);
        Task<ISugarQueryable<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression);
        Task<ISugarQueryable<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds);
        Task<ISugarQueryable<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true);
        Task<ISugarQueryable<TEntity>> Query(string strWhere, string strOrderByFileds);
        Task<ISugarQueryable<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, int intTop, string strOrderByFileds);
        Task<ISugarQueryable<TEntity>> Query(string strWhere, int intTop, string strOrderByFileds);
        Task<ISugarQueryable<TEntity>> QueryByPage(int intPageIndex, int intPageSize, string strOrderByFileds = null, string strWhere = null);
        Task<ISugarQueryable<TEntity>> QueryByPage(
          int intPageIndex = 0, int intPageSize = 20, string strOrderByFileds = null, Expression<Func<TEntity, bool>> whereExpression = null);

        Task<int> GetCount(Expression<Func<TEntity, bool>> exp = null);
        Task<int> GetCount(string strWhere = null);
    }
}
