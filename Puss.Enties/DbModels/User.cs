using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Puss.Enties
{
    ///<summary>
    ///用户表
    ///</summary>
    [SugarTable("User")]
    public partial class User
    {
           public User(){


           }
           /// <summary>
           /// Desc:ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int ID {get;set;}

           /// <summary>
           /// Desc:用户名
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string UserName {get;set;}

           /// <summary>
           /// Desc:密码
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string PassWord {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime CreateTime {get;set;}

           /// <summary>
           /// Desc:最后登陆时间
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime LoginTime {get;set;}

           /// <summary>
           /// Desc:IP
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string IP {get;set;}

           /// <summary>
           /// Desc:备注
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Remark {get;set;}

           /// <summary>
           /// Desc:权限
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Permission {get;set;}

           /// <summary>
           /// Desc:头像
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Portrait {get;set;}

           /// <summary>
           /// Desc:邮箱
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Email {get;set;}

           /// <summary>
           /// Desc:手机
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Phone {get;set;}

           /// <summary>
           /// Desc:QQ
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string QQ {get;set;}

           /// <summary>
           /// Desc:余额
           /// Default:
           /// Nullable:False
           /// </summary>           
           public decimal Money {get;set;}

           /// <summary>
           /// Desc:Token
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Token {get;set;}

    }
}
