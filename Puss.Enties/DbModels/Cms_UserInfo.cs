using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Sugar.Enties
{
    ///<summary>
    ///CMS用户表
    ///</summary>
    [SugarTable("Cms_UserInfo")]
    public partial class Cms_UserInfo
    {
           public Cms_UserInfo(){


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
           /// Desc:权限
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Permission {get;set;}

           /// <summary>
           /// Desc:领导
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Leader {get;set;}

           /// <summary>
           /// Desc:备注
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Remark {get;set;}

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
           /// Nullable:False
           /// </summary>           
           public string IP {get;set;}

    }
}
