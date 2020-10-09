using Blog.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.IServices.Base
{
    public interface IBaseServices<TEntity> where TEntity : class, new()
    {
        Task<TEntity> QueryById(object objId);
        Task<TEntity> QueryById(object objId, bool blnUseCache = false);
        Task<List<TEntity>> QueryByIds(object[] lstIds);

        int Add(TEntity model);
        Task<int> AddAsync(TEntity model);

        int DeleteById(object id);
        Task<int> DeleteByIdAsync(object id);
        Task<int> Delete(TEntity model);
        Task<int> Delete(Expression<Func<TEntity, bool>> whereExpression);
        Task<int> DeleteByIds(object[] ids);

        Task<int> Update(TEntity model);
        Task<int> Update(TEntity entity, string strWhere);
        Task<int> Update(TEntity entity, List<string> lstColumns = null, List<string> lstIgnoreColumns = null, string strWhere = "");

        Task<List<TEntity>> Query();
        Task<List<TEntity>> Query(string strWhere);
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression);
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds);
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true);
        Task<List<TEntity>> Query(string strWhere, string strOrderByFileds);
        Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, int intTop, string strOrderByFileds);
        Task<List<TEntity>> Query(string strWhere, int intTop, string strOrderByFileds);
        Task<PageResult<TEntity>> QueryByPage(int intPageIndex, int intPageSize, string strOrderByFileds = null, string strWhere = null);
        Task<PageResult<TEntity>> QueryByPage(
             int intPageIndex = 0, int intPageSize = 20, string strOrderByFileds = null, Expression<Func<TEntity, bool>> whereExpression = null);

        Task<int> GetCount(Expression<Func<TEntity, bool>> exp = null);
        Task<int> GetCount(string strWhere = null);
    }
}
