﻿using Puss.Enties;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Puss.Data.Config;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using log4net.Repository;
using log4net;
using log4net.Config;

namespace Puss.BusinessCore
{
    public class DbContext
    {
        public DbContext()
        {
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = GlobalsConfig.Configuration[ConfigurationKeys.Sql_Connection],
                DbType = DbType.SqlServer,
                InitKeyType = InitKeyType.Attribute,//从特性读取主键和自增列信息
                IsAutoCloseConnection = true,//开启自动释放模式和EF原理一样我就不多解释了
                SlaveConnectionConfigs = GetSonConnection(),//读写分离
            });

            //调式代码 用来打印SQL 
            Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                //Console.WriteLine(sql + "\r\n" +
                //    Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                //Console.WriteLine();
                ILoggerRepository Logger = new Log().GetLoggerRepository();
                LogManager.GetLogger(Logger.Name, "Sql").Info($"{sql}\r\n{Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value))}");
            };
        }

        //注意：不能写成静态的
        public SqlSugarClient Db;//用来处理事务多表查询和复杂的操作

        /// <summary>
        /// 获取数据库DB
        /// </summary>
        /// <returns></returns>
        public virtual SqlSugarClient GetInstance()
        {
            return Db;
        }

        protected List<SlaveConnectionConfig> GetSonConnection()
        {
            return JsonConvert.DeserializeObject<List<SlaveConnectionConfig>>(GlobalsConfig.Configuration[ConfigurationKeys.Sql_ConnectionSon]); ;
        }
    }

    public class DbContext<T> where T : class, new()
    {
        public DbContext()
        {
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = GlobalsConfig.Configuration[ConfigurationKeys.Sql_Connection],
                DbType = DbType.SqlServer,
                InitKeyType = InitKeyType.Attribute,//从特性读取主键和自增列信息
                IsAutoCloseConnection = true,//开启自动释放模式和EF原理一样我就不多解释了

            });
            //调式代码 用来打印SQL 
            Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                //Console.WriteLine(sql + "\r\n" +
                //    Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                //Console.WriteLine();
                ILoggerRepository Logger = new Log().GetLoggerRepository();
                LogManager.GetLogger(Logger.Name, "Sql").Info($"{sql}\r\n{Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value))}");
            };
        }
        //注意：不能写成静态的
        public SqlSugarClient Db;//用来处理事务多表查询和复杂的操作
	    public SimpleClient<T> CurrentDb { get { return new SimpleClient<T>(Db); } }//用来操作当前表的数据

       public SimpleClient<Category> CategoryDb { get { return new SimpleClient<Category>(Db); } }//用来处理Category表的常用操作
       public SimpleClient<Cms_Article> Cms_ArticleDb { get { return new SimpleClient<Cms_Article>(Db); } }//用来处理Cms_Article表的常用操作
       public SimpleClient<Cms_Category> Cms_CategoryDb { get { return new SimpleClient<Cms_Category>(Db); } }//用来处理Cms_Category表的常用操作
       public SimpleClient<Cms_Logger> Cms_LoggerDb { get { return new SimpleClient<Cms_Logger>(Db); } }//用来处理Cms_Logger表的常用操作
       public SimpleClient<Cms_Menu> Cms_MenuDb { get { return new SimpleClient<Cms_Menu>(Db); } }//用来处理Cms_Menu表的常用操作
       public SimpleClient<Cms_Role> Cms_RoleDb { get { return new SimpleClient<Cms_Role>(Db); } }//用来处理Cms_Role表的常用操作
       public SimpleClient<Cms_RoleMenu> Cms_RoleMenuDb { get { return new SimpleClient<Cms_RoleMenu>(Db); } }//用来处理Cms_RoleMenu表的常用操作
       public SimpleClient<Cms_Sysconfig> Cms_SysconfigDb { get { return new SimpleClient<Cms_Sysconfig>(Db); } }//用来处理Cms_Sysconfig表的常用操作
       public SimpleClient<Cms_UserInfo> Cms_UserInfoDb { get { return new SimpleClient<Cms_UserInfo>(Db); } }//用来处理Cms_UserInfo表的常用操作
       public SimpleClient<Code> CodeDb { get { return new SimpleClient<Code>(Db); } }//用来处理Code表的常用操作
       public SimpleClient<ExceptionLog> ExceptionLogDb { get { return new SimpleClient<ExceptionLog>(Db); } }//用来处理ExceptionLog表的常用操作
       public SimpleClient<Hot> HotDb { get { return new SimpleClient<Hot>(Db); } }//用来处理Hot表的常用操作
       public SimpleClient<KeyContent> KeyContentDb { get { return new SimpleClient<KeyContent>(Db); } }//用来处理KeyContent表的常用操作
       public SimpleClient<KeySection> KeySectionDb { get { return new SimpleClient<KeySection>(Db); } }//用来处理KeySection表的常用操作
       public SimpleClient<LogErrorDetails> LogErrorDetailsDb { get { return new SimpleClient<LogErrorDetails>(Db); } }//用来处理LogErrorDetails表的常用操作
       public SimpleClient<Logger> LoggerDb { get { return new SimpleClient<Logger>(Db); } }//用来处理Logger表的常用操作
       public SimpleClient<LogJobDetails> LogJobDetailsDb { get { return new SimpleClient<LogJobDetails>(Db); } }//用来处理LogJobDetails表的常用操作
       public SimpleClient<Order> OrderDb { get { return new SimpleClient<Order>(Db); } }//用来处理Order表的常用操作
       public SimpleClient<Shop> ShopDb { get { return new SimpleClient<Shop>(Db); } }//用来处理Shop表的常用操作
       public SimpleClient<ShoppingCart> ShoppingCartDb { get { return new SimpleClient<ShoppingCart>(Db); } }//用来处理ShoppingCart表的常用操作
       public SimpleClient<User> UserDb { get { return new SimpleClient<User>(Db); } }//用来处理User表的常用操作
       public SimpleClient<UserDetails> UserDetailsDb { get { return new SimpleClient<UserDetails>(Db); } }//用来处理UserDetails表的常用操作
       public SimpleClient<Tel> TelDb { get { return new SimpleClient<Tel>(Db); } }//用来处理Tel表的常用操作
       public SimpleClient<Movie_Cinemas> Movie_CinemasDb { get { return new SimpleClient<Movie_Cinemas>(Db); } }//用来处理Movie_Cinemas表的常用操作
       public SimpleClient<Movie_City> Movie_CityDb { get { return new SimpleClient<Movie_City>(Db); } }//用来处理Movie_City表的常用操作
       public SimpleClient<Movie_Shows> Movie_ShowsDb { get { return new SimpleClient<Movie_Shows>(Db); } }//用来处理Movie_Shows表的常用操作
       public SimpleClient<Movie_Film> Movie_FilmDb { get { return new SimpleClient<Movie_Film>(Db); } }//用来处理Movie_Film表的常用操作
       public SimpleClient<Movie_Member> Movie_MemberDb { get { return new SimpleClient<Movie_Member>(Db); } }//用来处理Movie_Member表的常用操作
        
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        public List<T> GetList()
        {
            return CurrentDb.GetList();
        }

        /// <summary>
        /// 异步获取所有
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> GetListAsync()
        {
            return await Db.Queryable<T>().ToListAsync();
        }

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <returns></returns>
        public T GetSingle(Expression<Func<T, bool>> whereExpression)
        {
            return CurrentDb.GetSingle(whereExpression);
        }


        /// <summary>
        /// 异步获取单个
        /// </summary>
        /// <returns></returns>
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> whereExpression)
        {
            return await Db.Queryable<T>().Where(whereExpression).SingleAsync();
        }

        /// <summary>
        /// 获取单个，为空自动自动初始化
        /// </summary>
        /// <returns></returns>
        public T GetSingleDefault(Expression<Func<T, bool>> whereExpression)
        {
            T temp = null;
            try
            {
                temp = CurrentDb.GetSingle(whereExpression);
            }
            catch
            {
            }
            return temp ?? new T();
        }

        /// <summary>
        /// 异步获取单个，为空自动自动初始化
        /// </summary>
        /// <returns></returns>
        public async Task<T> GetSingleDefaultAsync(Expression<Func<T, bool>> whereExpression)
        {
            T temp = null;
            try
            {
                temp = await Db.Queryable<T>().Where(whereExpression).SingleAsync();
            }
            catch
            {
            }
            return temp ?? new T();
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <returns></returns>
        public bool IsAny(Expression<Func<T, bool>> whereExpression)
        {
            return CurrentDb.IsAny(whereExpression);
        }

        /// <summary>
        /// 根据表达式查询
        /// </summary>
        /// <returns></returns>
        public List<T> GetList(Expression<Func<T, bool>> whereExpression)
        {
            return CurrentDb.GetList(whereExpression);
        }

        /// <summary>
        /// 异步根据表达式查询
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> whereExpression)
        {
            return await Db.Queryable<T>().Where(whereExpression).ToListAsync();
        }

        /// <summary>
        /// 根据表达式查询分页
        /// </summary>
        /// <returns></returns>
        public List<T> GetPageList(Expression<Func<T, bool>> whereExpression, PageModel pageModel)
        {
            return CurrentDb.GetPageList(whereExpression, pageModel);
        }

        /// <summary>
        /// 异步根据表达式查询分页
        /// </summary>
        /// <returns></returns>
        public async Task<List<T>> GetPageListAsync(Expression<Func<T, bool>> whereExpression, PageModel pageModel)
        {
            int totalNumber = 0;
            RefAsync<int> re = new RefAsync<int>(totalNumber);
            List<T> result = await Db.Queryable<T>().Where(whereExpression).ToPageListAsync(pageModel.PageIndex, pageModel.PageSize, re);
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
        public List<T> GetPageList(Expression<Func<T, bool>> whereExpression, PageModel pageModel, Expression<Func<T, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc)
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
        public async Task<List<T>> GetPageListAsync(Expression<Func<T, bool>> whereExpression, PageModel pageModel, Expression<Func<T, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc)
        {
            int totalNumber = 0;
            RefAsync<int> re = new RefAsync<int>(totalNumber);
            List<T> result = await Db.Queryable<T>().OrderByIF(orderByExpression != null, orderByExpression, orderByType).Where(whereExpression)
                .ToPageListAsync(pageModel.PageIndex, pageModel.PageSize, re);
            pageModel.PageCount = re.Value;
            return result;
        }


        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(dynamic id)
        {
            return CurrentDb.GetById(id);
        }

        /// <summary>
        /// 异步根据主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<T> GetByIdAsync(dynamic id)
        {
            return await Db.Queryable<T>().InSingleAsync(id);
        }

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Delete(dynamic id)
        {
            return CurrentDb.DeleteById(id);
        }


        /// <summary>
        /// 根据实体删除
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool Delete(T data)
        {
            return CurrentDb.Delete(data);
        }

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public bool Delete(dynamic[] ids)
        {
            return CurrentDb.AsDeleteable().In(ids).ExecuteCommand() > 0;
        }

        /// <summary>
        /// 根据表达式删除
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public bool Delete(Expression<Func<T, bool>> whereExpression)
        {
            return CurrentDb.Delete(whereExpression);
        }


        /// <summary>
        /// 根据实体更新，实体需要有主键
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Update(T obj)
        {
            return CurrentDb.Update(obj);
        }

        /// <summary>
        ///批量更新
        /// </summary>
        /// <param name="objs"></param>
        /// <returns></returns>
        public bool Update(List<T> objs)
        {
            return CurrentDb.UpdateRange(objs);
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Insert(T obj)
        {
            return CurrentDb.Insert(obj);
        }

        /// <summary>
        /// 插入返回ID
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int InsertReturnIdentity(T obj)
        {
            return CurrentDb.InsertReturnIdentity(obj);
        }

        /// <summary>
        /// 批量
        /// </summary>
        /// <param name="objs"></param>
        /// <returns></returns>
        public bool Insert(List<T> objs)
        {
            return CurrentDb.InsertRange(objs);
        }
    }
    
    public interface IDbContext<T>
    {
        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        List<T> GetList();

        /// <summary>
        /// 异步获取所有
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetListAsync();

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <returns></returns>
        T GetSingle(Expression<Func<T, bool>> whereExpression);


        /// <summary>
        /// 异步获取单个
        /// </summary>
        /// <returns></returns>
        Task<T> GetSingleAsync(Expression<Func<T, bool>> whereExpression);

        /// <summary>
        /// 获取单个，为空自动自动初始化
        /// </summary>
        /// <returns></returns>
        T GetSingleDefault(Expression<Func<T, bool>> whereExpression);

        /// <summary>
        /// 异步获取单个，为空自动自动初始化
        /// </summary>
        /// <returns></returns>
        Task<T> GetSingleDefaultAsync(Expression<Func<T, bool>> whereExpression);

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <returns></returns>
        bool IsAny(Expression<Func<T, bool>> whereExpression);

        /// <summary>
        /// 根据表达式查询
        /// </summary>
        /// <returns></returns>
        List<T> GetList(Expression<Func<T, bool>> whereExpression);

        /// <summary>
        /// 异步根据表达式查询
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetListAsync(Expression<Func<T, bool>> whereExpression);

        /// <summary>
        /// 根据表达式查询分页
        /// </summary>
        /// <returns></returns>
        List<T> GetPageList(Expression<Func<T, bool>> whereExpression, PageModel pageModel);

        /// <summary>
        /// 异步根据表达式查询分页
        /// </summary>
        /// <returns></returns>
        Task<List<T>> GetPageListAsync(Expression<Func<T, bool>> whereExpression, PageModel pageModel);

        /// <summary>
        /// 根据表达式查询分页并排序
        /// </summary>
        /// <param name="whereExpression">it</param>
        /// <param name="pageModel"></param>
        /// <param name="orderByExpression">it=>it.id或者it=>new{it.id,it.name}</param>
        /// <param name="orderByType">OrderByType.Desc</param>
        /// <returns></returns>
        List<T> GetPageList(Expression<Func<T, bool>> whereExpression, PageModel pageModel, Expression<Func<T, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc);

        /// <summary>
        /// 异步根据表达式查询分页并排序
        /// </summary>
        /// <param name="whereExpression">it</param>
        /// <param name="pageModel"></param>
        /// <param name="orderByExpression">it=>it.id或者it=>new{it.id,it.name}</param>
        /// <param name="orderByType">OrderByType.Desc</param>
        /// <returns></returns>
        Task<List<T>> GetPageListAsync(Expression<Func<T, bool>> whereExpression, PageModel pageModel, Expression<Func<T, object>> orderByExpression = null, OrderByType orderByType = OrderByType.Asc);


        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(dynamic id);

        /// <summary>
        /// 异步根据主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetByIdAsync(dynamic id);

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(dynamic id);


        /// <summary>
        /// 根据实体删除
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Delete(T data);

        /// <summary>
        /// 根据主键删除
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        bool Delete(dynamic[] ids);

        /// <summary>
        /// 根据表达式删除
        /// </summary>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        bool Delete(Expression<Func<T, bool>> whereExpression);


        /// <summary>
        /// 根据实体更新，实体需要有主键
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool Update(T obj);

        /// <summary>
        ///批量更新
        /// </summary>
        /// <param name="objs"></param>
        /// <returns></returns>
        bool Update(List<T> objs);

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool Insert(T obj);

        /// <summary>
        /// 插入返回ID
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        int InsertReturnIdentity(T obj);

        /// <summary>
        /// 批量
        /// </summary>
        /// <param name="objs"></param>
        /// <returns></returns>
        bool Insert(List<T> objs);
    }

    /// <summary>
    /// 日志
    /// </summary>
    public class Log 
    {
        private static ILoggerRepository _loggerRepository;

        public ILoggerRepository GetLoggerRepository()
        {
            if (_loggerRepository != null)
            {
                return _loggerRepository;
            }
            _loggerRepository = LogManager.CreateRepository(nameof(Log));
            XmlConfigurator.ConfigureAndWatch(_loggerRepository, new FileInfo("config/log4net.config"));
            return _loggerRepository;
        }
    }
}

