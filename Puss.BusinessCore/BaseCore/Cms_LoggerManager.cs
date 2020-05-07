using Puss.Enties;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Puss.BusinessCore
{
	public class Cms_LoggerManager : ICms_LoggerManager
	{
        public DbContext<Cms_Logger> CurrentDb { get { return new DbContext<Cms_Logger>(); } }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public virtual List<Cms_Logger> GetList()
        {
            return CurrentDb.GetList();
        }

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <returns></returns>
        public virtual Cms_Logger GetSingle(Expression<Func<Cms_Logger, bool>> whereExpression)
        {
            return CurrentDb.GetSingle(whereExpression);
        }


        /// <summary>
        /// 异步获取单个
        /// </summary>
        /// <returns></returns>
        public virtual async Task<Cms_Logger> GetSingleAsync(Expression<Func<Cms_Logger, bool>> whereExpression)
        {
            return await CurrentDb.GetSingleAsync(whereExpression);
        }

        /// <summary>
        /// 获取单个，为空自动自动初始化
        /// </summary>
        /// <returns></returns>
        public virtual Cms_Logger GetSingleDefault(Expression<Func<Cms_Logger, bool>> whereExpression)
        {
            return CurrentDb.GetSingleDefault(whereExpression);
        }

        /// <summary>
        /// 异步获取单个，为空自动自动初始化
        /// </summary>
        /// <returns></returns>
        public virtual async Task<Cms_Logger> GetSingleDefaultAsync(Expression<Func<Cms_Logger, bool>> whereExpression)
        {
            return await CurrentDb.GetSingleDefaultAsync(whereExpression);
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <returns></returns>
        public virtual bool IsAny(Expression<Func<Cms_Logger, bool>> whereExpression)
        {
            return CurrentDb.IsAny(whereExpression);
        }

        /// <summary>
        /// 根据表达式查询
        /// </summary>
        /// <returns></returns>
        public virtual List<Cms_Logger> GetList(Expression<Func<Cms_Logger, bool>> whereExpression)
        {
            return CurrentDb.GetList(whereExpression);
        }

        /// <summary>
        /// 异步根据表达式查询
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<Cms_Logger>> GetListAsync(Expression<Func<Cms_Logger, bool>> whereExpression)
        {
            return await CurrentDb.GetListAsync(whereExpression);
        }

        /// <summary>
        /// 根据表达式查询分页
        /// </summary>
        /// <returns></returns>
        public virtual List<Cms_Logger> GetPageList(Expression<Func<Cms_Logger, bool>> whereExpression, PageModel pageModel)
        {
            return CurrentDb.GetPageList(whereExpression, pageModel);
        }

        /// <summary>
        /// 异步根据表达式查询分页
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<Cms_Logger>> GetPageListAsync(Expression<Func<Cms_Logger, bool>> whereExpression, PageModel pageModel)
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
        public virtual List<Cms_Logger> GetPageList(Expression<Func<Cms_Logger, bool>> whereExpression, PageModel pageModel, Expression<Func<Cms_Logger, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc)
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
        public virtual async Task<List<Cms_Logger>> GetPageListAsync(Expression<Func<Cms_Logger, bool>> whereExpression, PageModel pageModel, Expression<Func<Cms_Logger, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await CurrentDb.GetPageListAsync(whereExpression, pageModel, orderByExpression, orderByType);
        }


        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <returns></returns>
        public virtual Cms_Logger GetById(dynamic id)
        {
            return CurrentDb.GetById(id);
        }

        /// <summary>
        /// 异步根据主键查询
        /// </summary>
        /// <returns></returns>
        public virtual async Task<Cms_Logger> GetByIdAsync(dynamic id)
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
        public virtual bool Delete(Cms_Logger data)
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
        public virtual bool Delete(Expression<Func<Cms_Logger, bool>> whereExpression)
        {
            return CurrentDb.Delete(whereExpression);
        }


        /// <summary>
        /// 根据实体更新，实体需要有主键
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Update(Cms_Logger obj)
        {
            return CurrentDb.Update(obj);
        }

        /// <summary>
        ///批量更新
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Update(List<Cms_Logger> objs)
        {
            return CurrentDb.Update(objs);
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Insert(Cms_Logger obj)
        {
            return CurrentDb.Insert(obj);
        }


        /// <summary>
        /// 批量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Insert(List<Cms_Logger> objs)
        {
            return CurrentDb.Insert(objs);
        }

        /// <summary>
        /// 获取数据库上下文
        /// </summary>
        /// <returns></returns>
        public virtual DbContext<Cms_Logger> GetDB()
        {
            return CurrentDb;
        }
    }

    public interface ICms_LoggerManager 
	{
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        List<Cms_Logger> GetList();

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <returns></returns>
        Cms_Logger GetSingle(Expression<Func<Cms_Logger, bool>> whereExpression);


        /// <summary>
        /// 异步获取单个
        /// </summary>
        /// <returns></returns>
        Task<Cms_Logger> GetSingleAsync(Expression<Func<Cms_Logger, bool>> whereExpression);

        /// <summary>
        /// 获取单个，为空自动自动初始化
        /// </summary>
        /// <returns></returns>
        Cms_Logger GetSingleDefault(Expression<Func<Cms_Logger, bool>> whereExpression);

        /// <summary>
        /// 异步获取单个，为空自动自动初始化
        /// </summary>
        /// <returns></returns>
        Task<Cms_Logger> GetSingleDefaultAsync(Expression<Func<Cms_Logger, bool>> whereExpression);

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <returns></returns>
        bool IsAny(Expression<Func<Cms_Logger, bool>> whereExpression);

        /// <summary>
        /// 根据表达式查询
        /// </summary>
        /// <returns></returns>
        List<Cms_Logger> GetList(Expression<Func<Cms_Logger, bool>> whereExpression);

        /// <summary>
        /// 异步根据表达式查询
        /// </summary>
        /// <returns></returns>
        Task<List<Cms_Logger>> GetListAsync(Expression<Func<Cms_Logger, bool>> whereExpression);

        /// <summary>
        /// 根据表达式查询分页
        /// </summary>
        /// <returns></returns>
        List<Cms_Logger> GetPageList(Expression<Func<Cms_Logger, bool>> whereExpression, PageModel pageModel);

        /// <summary>
        /// 异步根据表达式查询分页
        /// </summary>
        /// <returns></returns>
        Task<List<Cms_Logger>> GetPageListAsync(Expression<Func<Cms_Logger, bool>> whereExpression, PageModel pageModel);

        /// <summary>
        /// 根据表达式查询分页并排序
        /// </summary>
        /// <param name="whereExpression">it</param>
        /// <param name="pageModel"></param>
        /// <param name="orderByExpression">it=>it.id或者it=>new{it.id,it.name}</param>
        /// <param name="orderByType">OrderByType.Desc</param>
        /// <returns></returns>
        List<Cms_Logger> GetPageList(Expression<Func<Cms_Logger, bool>> whereExpression, PageModel pageModel, Expression<Func<Cms_Logger, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc);

        /// <summary>
        /// 异步根据表达式查询分页并排序
        /// </summary>
        /// <param name="whereExpression">it</param>
        /// <param name="pageModel"></param>
        /// <param name="orderByExpression">it=>it.id或者it=>new{it.id,it.name}</param>
        /// <param name="orderByType">OrderByType.Desc</param>
        /// <returns></returns>
        Task<List<Cms_Logger>> GetPageListAsync(Expression<Func<Cms_Logger, bool>> whereExpression, PageModel pageModel, Expression<Func<Cms_Logger, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <returns></returns>
        Cms_Logger GetById(dynamic id);

        /// <summary>
        /// 异步根据主键查询
        /// </summary>
        /// <returns></returns>
        Task<Cms_Logger> GetByIdAsync(dynamic id);

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
        bool Delete(Cms_Logger data);

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
        bool Delete(Expression<Func<Cms_Logger, bool>> whereExpression);


        /// <summary>
        /// 根据实体更新，实体需要有主键
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Update(Cms_Logger obj);

        /// <summary>
        ///批量更新
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Update(List<Cms_Logger> objs);

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Insert(Cms_Logger obj);


        /// <summary>
        /// 批量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Insert(List<Cms_Logger> objs);

        /// <summary>
        /// 获取数据库上下文
        /// </summary>
        /// <returns></returns>
        DbContext<Cms_Logger> GetDB();
    }
}