using Blog.Core.IRepository.Base;
using Blog.Core.Model;
using Blog.Core.Repository.Sugar;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Repository.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class, new()
    {
        //protected readonly DbContext _context;
        protected readonly ISqlSugarClient _db;
        //protected readonly SimpleClient<TEntity> _entity;
        public BaseRepository(ISqlSugarClient db)
        {
            //_context = DbContext.GetDbContext();
            _db = db;
        }
        /// <summary>
        /// 根据主键查询一条数据
        /// </summary>
        /// <param name="objId">主键</param>
        /// <returns></returns>
        public async Task<TEntity> QueryById(object objId)
        {
            return await _db.Queryable<TEntity>().InSingleAsync(objId);
        }
        /// <summary>
        /// 根据主键查询一条数据
        /// </summary>
        /// <param name="objId">主键</param>
        /// <param name="blnUseCache">是否使用缓存</param>
        /// <returns></returns>
        public async Task<TEntity> QueryById(object objId, bool blnUseCache = false)
        {
            return await _db.Queryable<TEntity>().WithCacheIF(blnUseCache).InSingleAsync(objId);
        }
        /// <summary>
        /// 根据主键数组查询数据
        /// </summary>
        /// <param name="lstIds">主键数组</param>
        /// <returns></returns>
        public async Task<ISugarQueryable<TEntity>> QueryByIds(object[] lstIds)
        {
            return await Task.Run(() => _db.Queryable<TEntity>().In(lstIds));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(TEntity model)
        {
            return _db.Insertable(model).ExecuteReturnIdentity();
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> AddAsync(TEntity model)
        {
            return await _db.Insertable(model).ExecuteReturnIdentityAsync();
        }

        /// <summary>
        /// 根据主键删除数据 
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public int DeleteById(object id)
        {
            return _db.Deleteable<TEntity>(id).ExecuteCommand();
        }
        /// <summary>
        /// 根据主键删除数据 
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<int> DeleteByIdAsync(object id)
        {
            return await _db.Deleteable<TEntity>(id).ExecuteCommandAsync();
        }
        /// <summary>
        /// 根据实体删除数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Delete(TEntity model)
        {
            return await _db.Deleteable<TEntity>(model).ExecuteCommandAsync();
        }
        /// <summary>
        /// 根据表达式删除
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public async Task<int> Delete(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await _db.Deleteable<TEntity>().Where(whereExpression).ExecuteCommandAsync();
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">主键数组</param>
        /// <returns></returns>
        public async Task<int> DeleteByIds(object[] ids)
        {
            return await _db.Deleteable<TEntity>().In(ids).ExecuteCommandAsync();
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(TEntity model)
        {
            return await _db.Updateable<TEntity>(model).ExecuteCommandAsync();
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public async Task<int> Update(TEntity entity, string strWhere)
        {
            return await _db.Updateable<TEntity>(entity).Where(strWhere).ExecuteCommandAsync();
        }
        /// <summary>
        /// 更新指定列字段数据
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="lstColumns">要更新的字段</param>
        /// <param name="lstIgnoreColumns">不需要更新的字段</param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public async Task<int> Update(TEntity entity, List<string> lstColumns = null, List<string> lstIgnoreColumns = null, string strWhere = "")
        {
            var update = _db.Updateable(entity);
            if (lstIgnoreColumns != null && lstIgnoreColumns.Count > 0)
            {
                update = update.IgnoreColumns(lstIgnoreColumns.ToArray());
            }
            if (lstColumns != null && lstColumns.Count > 0)
            {
                update = update.UpdateColumns(lstColumns.ToArray());
            }
            if (!string.IsNullOrEmpty(strWhere))
            {
                update = update.Where(strWhere);
            }
            return await update.ExecuteCommandAsync();
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public async Task<ISugarQueryable<TEntity>> Query()
        {
            return await Task.Run(() => _db.Queryable<TEntity>());
        }
        /// <summary>
        /// 根据条件查询数据列表
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public async Task<ISugarQueryable<TEntity>> Query(string strWhere)
        {
            return await Task.Run(() => _db.Queryable<TEntity>().WhereIF(!string.IsNullOrEmpty(strWhere), strWhere));
        }
        /// <summary>
        /// 表达式查询数据列表
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public async Task<ISugarQueryable<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await Task.Run(() => _db.Queryable<TEntity>().Where(whereExpression));
        }
        /// <summary>
        /// 排序查询一个列表
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="strOrderByFileds"></param>
        /// <returns></returns>
        public async Task<ISugarQueryable<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds)
        {
            return await Task.Run(() => _db.Queryable<TEntity>()
            .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
            .WhereIF(whereExpression != null, whereExpression));
        }
        /// <summary>
        /// 表达式排序查询一个列表
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public async Task<ISugarQueryable<TEntity>> Query(
            Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true)
        {
            return await Task.Run(() => _db.Queryable<TEntity>()
            .OrderByIF(orderByExpression != null, orderByExpression, isAsc ? OrderByType.Asc : OrderByType.Desc)
            .WhereIF(whereExpression != null, whereExpression));
        }
        /// <summary>
        /// 根据指定排序字段查询一个列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="strOrderByFileds"></param>
        /// <returns></returns>
        public async Task<ISugarQueryable<TEntity>> Query(string strWhere, string strOrderByFileds)
        {
            return await Task.Run(() => _db.Queryable<TEntity>()
            .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
            .WhereIF(!string.IsNullOrEmpty(strWhere), strWhere));
        }
        /// <summary>
        /// 查询前N条数据
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="intTop"></param>
        /// <param name="strOrderByFileds"></param>
        /// <returns></returns>
        public async Task<ISugarQueryable<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, int intTop, string strOrderByFileds)
        {
            return await Task.Run(() => _db.Queryable<TEntity>()
                .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
                .WhereIF(whereExpression != null, whereExpression).Take(intTop));
        }
        /// <summary>
        /// 查询前N条数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="intTop"></param>
        /// <param name="strOrderByFileds"></param>
        /// <returns></returns>
        public async Task<ISugarQueryable<TEntity>> Query(string strWhere, int intTop, string strOrderByFileds)
        {
            return await Task.Run(() => _db.Queryable<TEntity>().OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds).WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).Take(intTop));
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns></returns>
        public async Task<ISugarQueryable<TEntity>> QueryByPage(int intPageIndex, int intPageSize, string strOrderByFileds = null, string strWhere = null)
        {
            return await Task.Run(() => _db.Queryable<TEntity>()
                .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
                .WhereIF(!string.IsNullOrEmpty(strWhere), strWhere)
                .Skip(intPageSize * (intPageIndex - 1)).Take(intPageSize));
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="intPageIndex"></param>
        /// <param name="intPageSize"></param>
        /// <param name="strOrderByFileds"></param>
        /// <returns></returns>
        public async Task<ISugarQueryable<TEntity>> QueryByPage(int intPageIndex = 0, int intPageSize = 20, string strOrderByFileds = null, Expression<Func<TEntity, bool>> whereExpression = null)
        {
            return await Task.Run(() => _db.Queryable<TEntity>()
                 .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
                 .WhereIF(whereExpression != null, whereExpression)
                 .Skip(intPageSize * (intPageIndex - 1)).Take(intPageSize));
        }
        public async Task<int> GetCount(Expression<Func<TEntity, bool>> exp = null)
        {
            return await _db.Queryable<TEntity>().WhereIF(exp != null, exp).CountAsync();
        }
        public async Task<int> GetCount(string strWhere = null)
        {
            return await _db.Queryable<TEntity>().WhereIF(!string.IsNullOrEmpty(strWhere), strWhere).CountAsync();
        }
    }
}
