using Puss.Enties;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;

namespace Puss.BusinessCore
{
	public class UserDetailsManager : IUserDetailsManager
	{
        public SimpleClient<UserDetails> CurrentDb { get { return new DbContext<UserDetails>().UserDetailsDb; } }
        public SqlSugarClient Db { get { return new DbContext<UserDetails>().Db; } }


        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public virtual List<UserDetails> GetList()
        {
            return CurrentDb.GetList();
        }

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <returns></returns>
        public virtual UserDetails GetSingle(Expression<Func<UserDetails, bool>> whereExpression)
        {
            return CurrentDb.GetSingle(whereExpression);
        }


        /// <summary>
        /// 异步获取单个
        /// </summary>
        /// <returns></returns>
        public virtual async Task<UserDetails> GetSingleAsync(Expression<Func<UserDetails, bool>> whereExpression)
        {
            return await Db.Queryable<UserDetails>().Where(whereExpression).SingleAsync();
        }

        /// <summary>
        /// 获取单个，为空自动自动初始化
        /// </summary>
        /// <returns></returns>
        public virtual UserDetails GetSingleDefault(Expression<Func<UserDetails, bool>> whereExpression)
        {
            UserDetails temp = null;
            try
            {
                temp = CurrentDb.GetSingle(whereExpression);
            }
            catch
            {
            }
            return temp ?? new UserDetails();
        }

        /// <summary>
        /// 异步获取单个，为空自动自动初始化
        /// </summary>
        /// <returns></returns>
        public virtual async Task<UserDetails> GetSingleDefaultAsync(Expression<Func<UserDetails, bool>> whereExpression)
        {
            UserDetails temp = null;
            try
            {
                temp = await Db.Queryable<UserDetails>().Where(whereExpression).SingleAsync();
            }
            catch
            {
            }
            return temp ?? new UserDetails();
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <returns></returns>
        public virtual bool IsAny(Expression<Func<UserDetails, bool>> whereExpression)
        {
            return CurrentDb.IsAny(whereExpression);
        }

        /// <summary>
        /// 根据表达式查询
        /// </summary>
        /// <returns></returns>
        public virtual List<UserDetails> GetList(Expression<Func<UserDetails, bool>> whereExpression)
        {
            return CurrentDb.GetList(whereExpression);
        }

        /// <summary>
        /// 异步根据表达式查询
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<UserDetails>> GetListAsync(Expression<Func<UserDetails, bool>> whereExpression)
        {
            return await Db.Queryable<UserDetails>().Where(whereExpression).ToListAsync();
        }

        /// <summary>
        /// 根据表达式查询分页
        /// </summary>
        /// <returns></returns>
        public virtual List<UserDetails> GetPageList(Expression<Func<UserDetails, bool>> whereExpression, PageModel pageModel)
        {
            return CurrentDb.GetPageList(whereExpression, pageModel);
        }

        /// <summary>
        /// 异步根据表达式查询分页
        /// </summary>
        /// <returns></returns>
        public virtual async Task<List<UserDetails>> GetPageListAsync(Expression<Func<UserDetails, bool>> whereExpression, PageModel pageModel)
        {
            int totalNumber = 0;
            RefAsync<int> re = new RefAsync<int>(totalNumber);
            List<UserDetails> result = await Db.Queryable<UserDetails>().Where(whereExpression).ToPageListAsync(pageModel.PageIndex, pageModel.PageSize, re);
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
        public virtual List<UserDetails> GetPageList(Expression<Func<UserDetails, bool>> whereExpression, PageModel pageModel, Expression<Func<UserDetails, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc)
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
        public virtual async Task<List<UserDetails>> GetPageListAsync(Expression<Func<UserDetails, bool>> whereExpression, PageModel pageModel, Expression<Func<UserDetails, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc)
        {
            int totalNumber = 0;
            RefAsync<int> re = new RefAsync<int>(totalNumber);
            List<UserDetails> result = await Db.Queryable<UserDetails>().OrderByIF(orderByExpression != null, orderByExpression, orderByType).Where(whereExpression)
                .ToPageListAsync(pageModel.PageIndex, pageModel.PageSize, re);
            pageModel.PageCount = re.Value;
            return result;
        }


        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <returns></returns>
        public virtual UserDetails GetById(dynamic id)
        {
            return CurrentDb.GetById(id);
        }

        /// <summary>
        /// 异步根据主键查询
        /// </summary>
        /// <returns></returns>
        public virtual async Task<UserDetails> GetByIdAsync(dynamic id)
        {
            return await Db.Queryable<UserDetails>().InSingleAsync(id);
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
        public virtual bool Delete(UserDetails data)
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
        public virtual bool Delete(Expression<Func<UserDetails, bool>> whereExpression)
        {
            return CurrentDb.Delete(whereExpression);
        }


        /// <summary>
        /// 根据实体更新，实体需要有主键
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Update(UserDetails obj)
        {
            return CurrentDb.Update(obj);
        }

        /// <summary>
        ///批量更新
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Update(List<UserDetails> objs)
        {
            return CurrentDb.UpdateRange(objs);
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Insert(UserDetails obj)
        {
            return CurrentDb.Insert(obj);
        }


        /// <summary>
        /// 批量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual bool Insert(List<UserDetails> objs)
        {
            return CurrentDb.InsertRange(objs);
        }
    }

    public interface IUserDetailsManager 
	{
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        List<UserDetails> GetList();

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <returns></returns>
        UserDetails GetSingle(Expression<Func<UserDetails, bool>> whereExpression);


        /// <summary>
        /// 异步获取单个
        /// </summary>
        /// <returns></returns>
        Task<UserDetails> GetSingleAsync(Expression<Func<UserDetails, bool>> whereExpression);

        /// <summary>
        /// 获取单个，为空自动自动初始化
        /// </summary>
        /// <returns></returns>
        UserDetails GetSingleDefault(Expression<Func<UserDetails, bool>> whereExpression);

        /// <summary>
        /// 异步获取单个，为空自动自动初始化
        /// </summary>
        /// <returns></returns>
        Task<UserDetails> GetSingleDefaultAsync(Expression<Func<UserDetails, bool>> whereExpression);

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <returns></returns>
        bool IsAny(Expression<Func<UserDetails, bool>> whereExpression);

        /// <summary>
        /// 根据表达式查询
        /// </summary>
        /// <returns></returns>
        List<UserDetails> GetList(Expression<Func<UserDetails, bool>> whereExpression);

        /// <summary>
        /// 异步根据表达式查询
        /// </summary>
        /// <returns></returns>
        Task<List<UserDetails>> GetListAsync(Expression<Func<UserDetails, bool>> whereExpression);

        /// <summary>
        /// 根据表达式查询分页
        /// </summary>
        /// <returns></returns>
        List<UserDetails> GetPageList(Expression<Func<UserDetails, bool>> whereExpression, PageModel pageModel);

        /// <summary>
        /// 异步根据表达式查询分页
        /// </summary>
        /// <returns></returns>
        Task<List<UserDetails>> GetPageListAsync(Expression<Func<UserDetails, bool>> whereExpression, PageModel pageModel);

        /// <summary>
        /// 根据表达式查询分页并排序
        /// </summary>
        /// <param name="whereExpression">it</param>
        /// <param name="pageModel"></param>
        /// <param name="orderByExpression">it=>it.id或者it=>new{it.id,it.name}</param>
        /// <param name="orderByType">OrderByType.Desc</param>
        /// <returns></returns>
        List<UserDetails> GetPageList(Expression<Func<UserDetails, bool>> whereExpression, PageModel pageModel, Expression<Func<UserDetails, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc);

        /// <summary>
        /// 异步根据表达式查询分页并排序
        /// </summary>
        /// <param name="whereExpression">it</param>
        /// <param name="pageModel"></param>
        /// <param name="orderByExpression">it=>it.id或者it=>new{it.id,it.name}</param>
        /// <param name="orderByType">OrderByType.Desc</param>
        /// <returns></returns>
        Task<List<UserDetails>> GetPageListAsync(Expression<Func<UserDetails, bool>> whereExpression, PageModel pageModel, Expression<Func<UserDetails, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <returns></returns>
        UserDetails GetById(dynamic id);

        /// <summary>
        /// 异步根据主键查询
        /// </summary>
        /// <returns></returns>
        Task<UserDetails> GetByIdAsync(dynamic id);

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
        bool Delete(UserDetails data);

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
        bool Delete(Expression<Func<UserDetails, bool>> whereExpression);


        /// <summary>
        /// 根据实体更新，实体需要有主键
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Update(UserDetails obj);

        /// <summary>
        ///批量更新
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Update(List<UserDetails> objs);

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Insert(UserDetails obj);


        /// <summary>
        /// 批量
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Insert(List<UserDetails> objs);
    }
}