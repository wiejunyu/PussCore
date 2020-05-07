using Puss.Enties;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Puss.BusinessCore
{
	public class ExceptionLogManager : IExceptionLogManager
	{
        public DbContext<ExceptionLog> CurrentDb { get { return new DbContext<ExceptionLog>(); } }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public virtual List<ExceptionLog> GetList()
        {
            return CurrentDb.GetList();
        }

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <returns></returns>
        public virtual ExceptionLog GetSingle(Expression<Func<ExceptionLog, bool>> whereExpression)
        {
            return CurrentDb.GetSingle(whereExpression);
        }


        /// <summary>
        /// 异步获取单个
        /// </summary>
        /// <returns></returns>
        public virtual async Task<ExceptionLog> GetSingleAsync(Expression<Func<ExceptionLog, bool>> whereExpression)
        {
            return await CurrentDb.GetSingleAsync(whereExpression);
        }

        /// <summary>
        /// 获取单个，为空自动自动初始化
        /// </summary>
        /// <returns></returns>
        public virtual ExceptionLog GetSingleDefault(Expression<Func<ExceptionLog, bool>> whereExpression)
        {
            return CurrentDb.GetSingleDefault(whereExpression);
        }

        /// <summary>
        /// 异步获取单个，为空自动自动初始化
        /// </summary>
        /// <returns></returns>
        public virtual async Task<ExceptionLog> GetSingleDefaultAsync(Expression<Func<ExceptionLog, bool>> whereExpression)
        {
            return await CurrentDb.GetSingleDefaultAsync(whereExpression);
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <returns></returns>
        public virtual bool IsAny(Expression<Func<ExceptionLog, bool>> whereExpression)
        {
            return CurrentDb.IsAny(whereExpression);
        }

        /// <summary>
        /// 根据表达式查询
        /// </summary>
        /// <returns></returns>
        public virtual List<ExceptionLog> GetList(Expression<Func<ExceptionLog, bool>> whereExpression)
        {
            return CurrentDb.GetList(whereExpression);
        }

        /// <summary>
        /// 异步根据表达式查询
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<ExceptionLog>> GetListAsync(Expression<Func<ExceptionLog, bool>> whereExpression)
        {
            return await CurrentDb.GetListAsync(whereExpression);
        }

        /// <summary>
        /// 根据表达式查询分页
        /// </summary>
        /// <returns></returns>
        public virtual List<ExceptionLog> GetPageList(Expression<Func<ExceptionLog, bool>> whereExpression, PageModel pageModel)
        {
            return CurrentDb.GetPageList(whereExpression, pageModel);
        }

        /// <summary>
        /// 异步根据表达式查询分页
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<ExceptionLog>> GetPageListAsync(Expression<Func<ExceptionLog, bool>> whereExpression, PageModel pageModel)
        {
            return await CurrentDb.GetPageListAsync(whereExpression, pageModel);
        }

        /// <summary>
        /// 根据表达式查询分页并排序
        /// </summary>
        /// <param name="whereExpression">it</param>
        /// <param name="pageModel"></param>
        /// <param name="orderByExpression">it=>it.id或者it=>new{it.id,it.name}</param>
        /// <param name="orderByType">OrderByType.Desc</param>
        /// <returns></returns>
        public virtual List<ExceptionLog> GetPageList(Expression<Func<ExceptionLog, bool>> whereExpression, PageModel pageModel, Expression<Func<ExceptionLog, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc)
        {
            return CurrentDb.GetPageList(whereExpression, pageModel, orderByExpression, orderByType);
        }

        /// <summary>
        /// 异步根据表达式查询分页并排序
        /// </summary>
        /// <param name="whereExpression">it</param>
        /// <param name="pageModel"></param>
        /// <param name="orderByExpression">it=>it.id或者it=>new{it.id,it.name}</param>
        /// <param name="orderByType">OrderByType.Desc</param>
        /// <returns></returns>
        public virtual async Task<List<ExceptionLog>> GetPageListAsync(Expression<Func<ExceptionLog, bool>> whereExpression, PageModel pageModel, Expression<Func<ExceptionLog, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await CurrentDb.GetPageListAsync(whereExpression, pageModel, orderByExpression, orderByType);
        }


        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <returns></returns>
        public virtual ExceptionLog GetById(dynamic id)
        {
            return CurrentDb.GetById(id);
        }

        /// <summary>
        /// 异步根据主键查询
        /// </summary>
        /// <returns></returns>
        public virtual async Task<ExceptionLog> GetByIdAsync(dynamic id)
        {
            return await CurrentDb.GetByIdAsync(id);
        }

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Delete(dynamic id)
        {
            return CurrentDb.Delete(id);
        }


        /// <summary>
        /// 根据实体删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Delete(ExceptionLog data)
        {
            return CurrentDb.Delete(data);
        }

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Delete(dynamic[] ids)
        {
            return CurrentDb.Delete(ids);
        }

        /// <summary>
        /// 根据表达式删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Delete(Expression<Func<ExceptionLog, bool>> whereExpression)
        {
            return CurrentDb.Delete(whereExpression);
        }


        /// <summary>
        /// 根据实体更新，实体需要有主键
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Update(ExceptionLog obj)
        {
            return CurrentDb.Update(obj);
        }

        /// <summary>
        ///批量更新
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Update(List<ExceptionLog> objs)
        {
            return CurrentDb.Update(objs);
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Insert(ExceptionLog obj)
        {
            return CurrentDb.Insert(obj);
        }


        /// <summary>
        /// 批量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Insert(List<ExceptionLog> objs)
        {
            return CurrentDb.Insert(objs);
        }

        /// <summary>
        /// 获取数据库上下文
        /// </summary>
        /// <returns></returns>
        public virtual DbContext<ExceptionLog> GetDB()
        {
            return CurrentDb;
        }
    }

    public interface IExceptionLogManager 
	{
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        List<ExceptionLog> GetList();

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <returns></returns>
        ExceptionLog GetSingle(Expression<Func<ExceptionLog, bool>> whereExpression);


        /// <summary>
        /// 异步获取单个
        /// </summary>
        /// <returns></returns>
        Task<ExceptionLog> GetSingleAsync(Expression<Func<ExceptionLog, bool>> whereExpression);

        /// <summary>
        /// 获取单个，为空自动自动初始化
        /// </summary>
        /// <returns></returns>
        ExceptionLog GetSingleDefault(Expression<Func<ExceptionLog, bool>> whereExpression);

        /// <summary>
        /// 异步获取单个，为空自动自动初始化
        /// </summary>
        /// <returns></returns>
        Task<ExceptionLog> GetSingleDefaultAsync(Expression<Func<ExceptionLog, bool>> whereExpression);

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <returns></returns>
        bool IsAny(Expression<Func<ExceptionLog, bool>> whereExpression);

        /// <summary>
        /// 根据表达式查询
        /// </summary>
        /// <returns></returns>
        List<ExceptionLog> GetList(Expression<Func<ExceptionLog, bool>> whereExpression);

        /// <summary>
        /// 异步根据表达式查询
        /// </summary>
        /// <returns></returns>
        Task<List<ExceptionLog>> GetListAsync(Expression<Func<ExceptionLog, bool>> whereExpression);

        /// <summary>
        /// 根据表达式查询分页
        /// </summary>
        /// <returns></returns>
        List<ExceptionLog> GetPageList(Expression<Func<ExceptionLog, bool>> whereExpression, PageModel pageModel);

        /// <summary>
        /// 异步根据表达式查询分页
        /// </summary>
        /// <returns></returns>
        Task<List<ExceptionLog>> GetPageListAsync(Expression<Func<ExceptionLog, bool>> whereExpression, PageModel pageModel);

        /// <summary>
        /// 根据表达式查询分页并排序
        /// </summary>
        /// <param name="whereExpression">it</param>
        /// <param name="pageModel"></param>
        /// <param name="orderByExpression">it=>it.id或者it=>new{it.id,it.name}</param>
        /// <param name="orderByType">OrderByType.Desc</param>
        /// <returns></returns>
        List<ExceptionLog> GetPageList(Expression<Func<ExceptionLog, bool>> whereExpression, PageModel pageModel, Expression<Func<ExceptionLog, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc);

        /// <summary>
        /// 异步根据表达式查询分页并排序
        /// </summary>
        /// <param name="whereExpression">it</param>
        /// <param name="pageModel"></param>
        /// <param name="orderByExpression">it=>it.id或者it=>new{it.id,it.name}</param>
        /// <param name="orderByType">OrderByType.Desc</param>
        /// <returns></returns>
        Task<List<ExceptionLog>> GetPageListAsync(Expression<Func<ExceptionLog, bool>> whereExpression, PageModel pageModel, Expression<Func<ExceptionLog, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <returns></returns>
        ExceptionLog GetById(dynamic id);

        /// <summary>
        /// 异步根据主键查询
        /// </summary>
        /// <returns></returns>
        Task<ExceptionLog> GetByIdAsync(dynamic id);

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(dynamic id);


        /// <summary>
        /// 根据实体删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(ExceptionLog data);

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(dynamic[] ids);

        /// <summary>
        /// 根据表达式删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(Expression<Func<ExceptionLog, bool>> whereExpression);


        /// <summary>
        /// 根据实体更新，实体需要有主键
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Update(ExceptionLog obj);

        /// <summary>
        ///批量更新
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Update(List<ExceptionLog> objs);

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Insert(ExceptionLog obj);


        /// <summary>
        /// 批量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Insert(List<ExceptionLog> objs);

        /// <summary>
        /// 获取数据库上下文
        /// </summary>
        /// <returns></returns>
        DbContext<ExceptionLog> GetDB();
    }
}