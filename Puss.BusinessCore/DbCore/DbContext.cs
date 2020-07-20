using Puss.Enties;
using SqlSugar;
using System;
using System.Collections.Generic;
using Puss.Data.Config;
using Newtonsoft.Json;
using System.Linq;

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
            Db.Aop.OnLogExecuted = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" +
                    Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
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
                Console.WriteLine(sql + "\r\n" +
                    Db.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
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
    }
}

