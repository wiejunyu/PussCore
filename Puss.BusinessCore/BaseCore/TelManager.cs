﻿using Puss.Enties;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Puss.BusinessCore
{
	public class TelManager : ITelManager
	{
        public SimpleClient<Tel> CurrentDb { get { return new DbContext<Tel>().TelDb; } }
        public SqlSugarClient Db { get { return new DbContext<Tel>().Db; } }


        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public virtual List<Tel> GetList()
        {
            return CurrentDb.GetList();
        }

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <returns></returns>
        public virtual Tel GetSingle(Expression<Func<Tel, bool>> whereExpression)
        {
            return CurrentDb.GetSingle(whereExpression);
        }


        /// <summary>
        /// 异步获取单个
        /// </summary>
        /// <returns></returns>
        public virtual async Task<Tel> GetSingleAsync(Expression<Func<Tel, bool>> whereExpression)
        {
            return await Db.Queryable<Tel>().Where(whereExpression).SingleAsync();
        }

        /// <summary>
        /// 获取单个，为空自动自动初始化
        /// </summary>
        /// <returns></returns>
        public virtual Tel GetSingleDefault(Expression<Func<Tel, bool>> whereExpression)
        {
            Tel temp = null;
            try
            {
                temp = CurrentDb.GetSingle(whereExpression);
            }
            catch
            {
            }
            return temp ?? new Tel();
        }

        /// <summary>
        /// 异步获取单个，为空自动自动初始化
        /// </summary>
        /// <returns></returns>
        public virtual async Task<Tel> GetSingleDefaultAsync(Expression<Func<Tel, bool>> whereExpression)
        {
            Tel temp = null;
            try
            {
                temp = await Db.Queryable<Tel>().Where(whereExpression).SingleAsync();
            }
            catch
            {
            }
            return temp ?? new Tel();
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <returns></returns>
        public virtual bool IsAny(Expression<Func<Tel, bool>> whereExpression)
        {
            return CurrentDb.IsAny(whereExpression);
        }

        /// <summary>
        /// 根据表达式查询
        /// </summary>
        /// <returns></returns>
        public virtual List<Tel> GetList(Expression<Func<Tel, bool>> whereExpression)
        {
            return CurrentDb.GetList(whereExpression);
        }

        /// <summary>
        /// 异步根据表达式查询
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<Tel>> GetListAsync(Expression<Func<Tel, bool>> whereExpression)
        {
            return await Db.Queryable<Tel>().Where(whereExpression).ToListAsync();
        }

        /// <summary>
        /// 根据表达式查询分页
        /// </summary>
        /// <returns></returns>
        public virtual List<Tel> GetPageList(Expression<Func<Tel, bool>> whereExpression, PageModel pageModel)
        {
            return CurrentDb.GetPageList(whereExpression, pageModel);
        }

        /// <summary>
        /// 异步根据表达式查询分页
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<Tel>> GetPageListAsync(Expression<Func<Tel, bool>> whereExpression, PageModel pageModel)
        {
            int totalNumber = 0;
            RefAsync<int> re = new RefAsync<int>(totalNumber);
            List<Tel> result = await Db.Queryable<Tel>().Where(whereExpression).ToPageListAsync(pageModel.PageIndex, pageModel.PageSize, re);
            pageModel.PageCount = re.Value;
            return result;
        }

        /// <summary>
        /// 根据表达式查询分页并排序
        /// </summary>
        /// <param name="whereExpression">it</param>
        /// <param name="pageModel"></param>
        /// <param name="orderByExpression">it=>it.id或者it=>new{it.id,it.name}</param>
        /// <param name="orderByType">OrderByType.Desc</param>
        /// <returns></returns>
        public virtual List<Tel> GetPageList(Expression<Func<Tel, bool>> whereExpression, PageModel pageModel, Expression<Func<Tel, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc)
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
        public virtual async Task<List<Tel>> GetPageListAsync(Expression<Func<Tel, bool>> whereExpression, PageModel pageModel, Expression<Func<Tel, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc)
        {
            int totalNumber = 0;
            RefAsync<int> re = new RefAsync<int>(totalNumber);
            List<Tel> result = await Db.Queryable<Tel>().OrderByIF(orderByExpression != null, orderByExpression, orderByType).Where(whereExpression)
                .ToPageListAsync(pageModel.PageIndex, pageModel.PageSize, re);
            pageModel.PageCount = re.Value;
            return result;
        }


        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <returns></returns>
        public virtual Tel GetById(dynamic id)
        {
            return CurrentDb.GetById(id);
        }

        /// <summary>
        /// 异步根据主键查询
        /// </summary>
        /// <returns></returns>
        public virtual async Task<Tel> GetByIdAsync(dynamic id)
        {
            return await Db.Queryable<Tel>().InSingleAsync(id);
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
        public virtual bool Delete(Tel data)
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
            return CurrentDb.AsDeleteable().In(ids).ExecuteCommand() > 0;
        }

        /// <summary>
        /// 根据表达式删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Delete(Expression<Func<Tel, bool>> whereExpression)
        {
            return CurrentDb.Delete(whereExpression);
        }


        /// <summary>
        /// 根据实体更新，实体需要有主键
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Update(Tel obj)
        {
            return CurrentDb.Update(obj);
        }

        /// <summary>
        ///批量更新
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Update(List<Tel> objs)
        {
            return CurrentDb.UpdateRange(objs);
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Insert(Tel obj)
        {
            return CurrentDb.Insert(obj);
        }


        /// <summary>
        /// 批量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Insert(List<Tel> objs)
        {
            return CurrentDb.InsertRange(objs);
        }
    }

    public interface ITelManager 
	{
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        List<Tel> GetList();

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <returns></returns>
        Tel GetSingle(Expression<Func<Tel, bool>> whereExpression);


        /// <summary>
        /// 异步获取单个
        /// </summary>
        /// <returns></returns>
        Task<Tel> GetSingleAsync(Expression<Func<Tel, bool>> whereExpression);

        /// <summary>
        /// 获取单个，为空自动自动初始化
        /// </summary>
        /// <returns></returns>
        Tel GetSingleDefault(Expression<Func<Tel, bool>> whereExpression);

        /// <summary>
        /// 异步获取单个，为空自动自动初始化
        /// </summary>
        /// <returns></returns>
        Task<Tel> GetSingleDefaultAsync(Expression<Func<Tel, bool>> whereExpression);

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <returns></returns>
        bool IsAny(Expression<Func<Tel, bool>> whereExpression);

        /// <summary>
        /// 根据表达式查询
        /// </summary>
        /// <returns></returns>
        List<Tel> GetList(Expression<Func<Tel, bool>> whereExpression);

        /// <summary>
        /// 异步根据表达式查询
        /// </summary>
        /// <returns></returns>
        Task<List<Tel>> GetListAsync(Expression<Func<Tel, bool>> whereExpression);

        /// <summary>
        /// 根据表达式查询分页
        /// </summary>
        /// <returns></returns>
        List<Tel> GetPageList(Expression<Func<Tel, bool>> whereExpression, PageModel pageModel);

        /// <summary>
        /// 异步根据表达式查询分页
        /// </summary>
        /// <returns></returns>
        Task<List<Tel>> GetPageListAsync(Expression<Func<Tel, bool>> whereExpression, PageModel pageModel);

        /// <summary>
        /// 根据表达式查询分页并排序
        /// </summary>
        /// <param name="whereExpression">it</param>
        /// <param name="pageModel"></param>
        /// <param name="orderByExpression">it=>it.id或者it=>new{it.id,it.name}</param>
        /// <param name="orderByType">OrderByType.Desc</param>
        /// <returns></returns>
        List<Tel> GetPageList(Expression<Func<Tel, bool>> whereExpression, PageModel pageModel, Expression<Func<Tel, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc);

        /// <summary>
        /// 异步根据表达式查询分页并排序
        /// </summary>
        /// <param name="whereExpression">it</param>
        /// <param name="pageModel"></param>
        /// <param name="orderByExpression">it=>it.id或者it=>new{it.id,it.name}</param>
        /// <param name="orderByType">OrderByType.Desc</param>
        /// <returns></returns>
        Task<List<Tel>> GetPageListAsync(Expression<Func<Tel, bool>> whereExpression, PageModel pageModel, Expression<Func<Tel, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <returns></returns>
        Tel GetById(dynamic id);

        /// <summary>
        /// 异步根据主键查询
        /// </summary>
        /// <returns></returns>
        Task<Tel> GetByIdAsync(dynamic id);

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
        bool Delete(Tel data);

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
        bool Delete(Expression<Func<Tel, bool>> whereExpression);


        /// <summary>
        /// 根据实体更新，实体需要有主键
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Update(Tel obj);

        /// <summary>
        ///批量更新
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Update(List<Tel> objs);

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Insert(Tel obj);


        /// <summary>
        /// 批量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Insert(List<Tel> objs);
    }
}