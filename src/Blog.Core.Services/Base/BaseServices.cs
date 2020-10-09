using Blog.Core.IRepository.Base;
using Blog.Core.IServices.Base;
using Blog.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Core.Services.Base
{
    public class BaseServices<TEntity> : IBaseServices<TEntity> where TEntity : class, new()
    {
        protected readonly IBaseRepository<TEntity> _baseRepository;
        public BaseServices(IBaseRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        /// <summary>
        /// 根据主键查询一条数据
        /// </summary>
        /// <param name="objId">主键</param>
        /// <returns></returns>
        public async Task<TEntity> QueryById(object objId)
        {
            return await _baseRepository.QueryById(objId);
        }
        /// <summary>
        /// 根据主键查询一条数据
        /// </summary>
        /// <param name="objId">主键</param>
        /// <param name="blnUseCache">是否使用缓存</param>
        /// <returns></returns>
        public async Task<TEntity> QueryById(object objId, bool blnUseCache = false)
        {
            return await _baseRepository.QueryById(objId, blnUseCache);
        }
        /// <summary>
        /// 根据主键数组查询数据
        /// </summary>
        /// <param name="lstIds">主键数组</param>
        /// <returns></returns>
        public async Task<List<TEntity>> QueryByIds(object[] lstIds)
        {
            return (await _baseRepository.QueryByIds(lstIds)).ToList();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Add(TEntity model)
        {

            return _baseRepository.Add(model);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> AddAsync(TEntity model)
        {

            return await _baseRepository.AddAsync(model);
        }
        /// <summary>
        /// 根据主键删除数据 
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public int DeleteById(object id)
        {
            return _baseRepository.DeleteById(id);
        }
        /// <summary>
        /// 根据主键删除数据 
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns></returns>
        public async Task<int> DeleteByIdAsync(object id)
        {
            return await _baseRepository.DeleteByIdAsync(id);
        }
        /// <summary>
        /// 根据实体删除数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Delete(TEntity model)
        {
            return await _baseRepository.Delete(model);
        }
        /// <summary>
        /// 根据表达式删除
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public async Task<int> Delete(Expression<Func<TEntity, bool>> whereExpression)
        {
            return await _baseRepository.Delete(whereExpression);
        }
        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">主键数组</param>
        /// <returns></returns>
        public async Task<int> DeleteByIds(object[] ids)
        {
            return await _baseRepository.DeleteByIds(ids);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<int> Update(TEntity model)
        {
            return await _baseRepository.Update(model);
        }
        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="strWhere"></param>
        /// <returns></returns>
        public async Task<int> Update(TEntity entity, string strWhere)
        {
            return await _baseRepository.Update(entity, strWhere);
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
            return await _baseRepository.Update(entity, lstColumns, lstIgnoreColumns, strWhere);
        }

        /// <summary>
        /// 查询所有数据
        /// </summary>
        /// <returns></returns>
        public async Task<List<TEntity>> Query()
        {
            return (await _baseRepository.Query()).ToList();
        }
        /// <summary>
        /// 根据条件查询数据列表
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <returns></returns>
        public async Task<List<TEntity>> Query(string strWhere)
        {
            return (await _baseRepository.Query(strWhere)).ToList();
        }
        /// <summary>
        /// 表达式查询数据列表
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression)
        {
            return (await _baseRepository.Query(whereExpression)).ToList();
        }
        /// <summary>
        /// 排序查询一个列表
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="strOrderByFileds"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, string strOrderByFileds)
        {
            return (await _baseRepository.Query(whereExpression, strOrderByFileds)).ToList();
        }
        /// <summary>
        /// 表达式排序查询一个列表
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="orderByExpression"></param>
        /// <param name="isAsc"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> Query(
            Expression<Func<TEntity, bool>> whereExpression, Expression<Func<TEntity, object>> orderByExpression, bool isAsc = true)
        {
            return (await _baseRepository.Query(whereExpression, orderByExpression, isAsc)).ToList();
        }
        /// <summary>
        /// 根据指定排序字段查询一个列表
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="strOrderByFileds"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> Query(string strWhere, string strOrderByFileds)
        {
            return (await _baseRepository.Query(strWhere, strOrderByFileds)).ToList();
        }
        /// <summary>
        /// 查询前N条数据
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="intTop"></param>
        /// <param name="strOrderByFileds"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> Query(Expression<Func<TEntity, bool>> whereExpression, int intTop, string strOrderByFileds)
        {
            return (await _baseRepository.Query(whereExpression, intTop, strOrderByFileds)).ToList();
        }
        /// <summary>
        /// 查询前N条数据
        /// </summary>
        /// <param name="strWhere"></param>
        /// <param name="intTop"></param>
        /// <param name="strOrderByFileds"></param>
        /// <returns></returns>
        public async Task<List<TEntity>> Query(string strWhere, int intTop, string strOrderByFileds)
        {
            return (await _baseRepository.Query(strWhere, intTop, strOrderByFileds)).ToList();
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">页大小</param>
        /// <param name="strOrderByFileds">排序字段，如name asc,age desc</param>
        /// <returns></returns>
        public async Task<PageResult<TEntity>> QueryByPage(int intPageIndex, int intPageSize, string strOrderByFileds = null, string strWhere = null)
        {
            var list = await _baseRepository.QueryByPage(intPageIndex, intPageSize, strOrderByFileds, strWhere);

            var totalCount = await GetCount(strWhere);
            var pageCount = Convert.ToInt32(Math.Ceiling(totalCount * 1.0m / intPageSize));

            return new PageResult<TEntity>() { Data = list.ToList(), TotalCount = totalCount, PageIndex = intPageIndex, PageCount = pageCount, PageSize = intPageSize };
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <param name="intPageIndex"></param>
        /// <param name="intPageSize"></param>
        /// <param name="strOrderByFileds"></param>
        /// <returns></returns>
        public async Task<PageResult<TEntity>> QueryByPage(int intPageIndex = 0, int intPageSize = 20, string strOrderByFileds = null, Expression<Func<TEntity, bool>> whereExpression = null)
        {
            var list = await _baseRepository.QueryByPage(intPageIndex, intPageSize, strOrderByFileds, whereExpression);

            var totalCount = await GetCount(whereExpression);
            var pageCount = Convert.ToInt32(Math.Ceiling(totalCount * 1.0m / intPageSize));

            return new PageResult<TEntity>() { Data = list.ToList(), TotalCount = totalCount, PageIndex = intPageIndex, PageCount = pageCount, PageSize = intPageSize };
        }
        public async Task<int> GetCount(Expression<Func<TEntity, bool>> exp = null)
        {
            return await _baseRepository.GetCount(exp);
        }
        public async Task<int> GetCount(string strWhere = null)
        {
            return await _baseRepository.GetCount(strWhere);
        }
    }
}
