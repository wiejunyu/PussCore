using Puss.Enties;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Puss.BusinessCore
{
	public class CodeManager : ICodeManager
	{
        public DbContext<Code> CurrentDb { get { return new DbContext<Code>(); } }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public virtual List<Code> GetList()
        {
            return CurrentDb.GetList();
        }

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <returns></returns>
        public virtual Code GetSingle(Expression<Func<Code, bool>> whereExpression)
        {
            return CurrentDb.GetSingle(whereExpression);
        }


        /// <summary>
        /// 异步获取单个
        /// </summary>
        /// <returns></returns>
        public virtual async Task<Code> GetSingleAsync(Expression<Func<Code, bool>> whereExpression)
        {
            return await CurrentDb.GetSingleAsync(whereExpression);
        }

        /// <summary>
        /// 获取单个，为空自动自动初始化
        /// </summary>
        /// <returns></returns>
        public virtual Code GetSingleDefault(Expression<Func<Code, bool>> whereExpression)
        {
            return CurrentDb.GetSingleDefault(whereExpression);
        }

        /// <summary>
        /// 异步获取单个，为空自动自动初始化
        /// </summary>
        /// <returns></returns>
        public virtual async Task<Code> GetSingleDefaultAsync(Expression<Func<Code, bool>> whereExpression)
        {
            return await CurrentDb.GetSingleDefaultAsync(whereExpression);
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <returns></returns>
        public virtual bool IsAny(Expression<Func<Code, bool>> whereExpression)
        {
            return CurrentDb.IsAny(whereExpression);
        }

        /// <summary>
        /// 根据表达式查询
        /// </summary>
        /// <returns></returns>
        public virtual List<Code> GetList(Expression<Func<Code, bool>> whereExpression)
        {
            return CurrentDb.GetList(whereExpression);
        }

        /// <summary>
        /// 异步根据表达式查询
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<Code>> GetListAsync(Expression<Func<Code, bool>> whereExpression)
        {
            return await CurrentDb.GetListAsync(whereExpression);
        }

        /// <summary>
        /// 根据表达式查询分页
        /// </summary>
        /// <returns></returns>
        public virtual List<Code> GetPageList(Expression<Func<Code, bool>> whereExpression, PageModel pageModel)
        {
            return CurrentDb.GetPageList(whereExpression, pageModel);
        }

        /// <summary>
        /// 异步根据表达式查询分页
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<Code>> GetPageListAsync(Expression<Func<Code, bool>> whereExpression, PageModel pageModel)
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
        public virtual List<Code> GetPageList(Expression<Func<Code, bool>> whereExpression, PageModel pageModel, Expression<Func<Code, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc)
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
        public virtual async Task<List<Code>> GetPageListAsync(Expression<Func<Code, bool>> whereExpression, PageModel pageModel, Expression<Func<Code, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc)
        {
            return await CurrentDb.GetPageListAsync(whereExpression, pageModel, orderByExpression, orderByType);
        }


        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <returns></returns>
        public virtual Code GetById(dynamic id)
        {
            return CurrentDb.GetById(id);
        }

        /// <summary>
        /// 异步根据主键查询
        /// </summary>
        /// <returns></returns>
        public virtual async Task<Code> GetByIdAsync(dynamic id)
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
        public virtual bool Delete(Code data)
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
        public virtual bool Delete(Expression<Func<Code, bool>> whereExpression)
        {
            return CurrentDb.Delete(whereExpression);
        }


        /// <summary>
        /// 根据实体更新，实体需要有主键
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Update(Code obj)
        {
            return CurrentDb.Update(obj);
        }

        /// <summary>
        ///批量更新
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Update(List<Code> objs)
        {
            return CurrentDb.Update(objs);
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Insert(Code obj)
        {
            return CurrentDb.Insert(obj);
        }


        /// <summary>
        /// 批量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Insert(List<Code> objs)
        {
            return CurrentDb.Insert(objs);
        }

        /// <summary>
        /// 获取数据库上下文
        /// </summary>
        /// <returns></returns>
        public virtual DbContext<Code> GetDB()
        {
            return CurrentDb;
        }
    }

    public interface ICodeManager 
	{
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        List<Code> GetList();

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <returns></returns>
        Code GetSingle(Expression<Func<Code, bool>> whereExpression);


        /// <summary>
        /// 异步获取单个
        /// </summary>
        /// <returns></returns>
        Task<Code> GetSingleAsync(Expression<Func<Code, bool>> whereExpression);

        /// <summary>
        /// 获取单个，为空自动自动初始化
        /// </summary>
        /// <returns></returns>
        Code GetSingleDefault(Expression<Func<Code, bool>> whereExpression);

        /// <summary>
        /// 异步获取单个，为空自动自动初始化
        /// </summary>
        /// <returns></returns>
        Task<Code> GetSingleDefaultAsync(Expression<Func<Code, bool>> whereExpression);

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <returns></returns>
        bool IsAny(Expression<Func<Code, bool>> whereExpression);

        /// <summary>
        /// 根据表达式查询
        /// </summary>
        /// <returns></returns>
        List<Code> GetList(Expression<Func<Code, bool>> whereExpression);

        /// <summary>
        /// 异步根据表达式查询
        /// </summary>
        /// <returns></returns>
        Task<List<Code>> GetListAsync(Expression<Func<Code, bool>> whereExpression);

        /// <summary>
        /// 根据表达式查询分页
        /// </summary>
        /// <returns></returns>
        List<Code> GetPageList(Expression<Func<Code, bool>> whereExpression, PageModel pageModel);

        /// <summary>
        /// 异步根据表达式查询分页
        /// </summary>
        /// <returns></returns>
        Task<List<Code>> GetPageListAsync(Expression<Func<Code, bool>> whereExpression, PageModel pageModel);

        /// <summary>
        /// 根据表达式查询分页并排序
        /// </summary>
        /// <param name="whereExpression">it</param>
        /// <param name="pageModel"></param>
        /// <param name="orderByExpression">it=>it.id或者it=>new{it.id,it.name}</param>
        /// <param name="orderByType">OrderByType.Desc</param>
        /// <returns></returns>
        List<Code> GetPageList(Expression<Func<Code, bool>> whereExpression, PageModel pageModel, Expression<Func<Code, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc);

        /// <summary>
        /// 异步根据表达式查询分页并排序
        /// </summary>
        /// <param name="whereExpression">it</param>
        /// <param name="pageModel"></param>
        /// <param name="orderByExpression">it=>it.id或者it=>new{it.id,it.name}</param>
        /// <param name="orderByType">OrderByType.Desc</param>
        /// <returns></returns>
        Task<List<Code>> GetPageListAsync(Expression<Func<Code, bool>> whereExpression, PageModel pageModel, Expression<Func<Code, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <returns></returns>
        Code GetById(dynamic id);

        /// <summary>
        /// 异步根据主键查询
        /// </summary>
        /// <returns></returns>
        Task<Code> GetByIdAsync(dynamic id);

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
        bool Delete(Code data);

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
        bool Delete(Expression<Func<Code, bool>> whereExpression);


        /// <summary>
        /// 根据实体更新，实体需要有主键
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Update(Code obj);

        /// <summary>
        ///批量更新
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Update(List<Code> objs);

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Insert(Code obj);


        /// <summary>
        /// 批量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Insert(List<Code> objs);

        /// <summary>
        /// 获取数据库上下文
        /// </summary>
        /// <returns></returns>
        DbContext<Code> GetDB();
    }
}