using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Sugar.Enties
{
    ///<summary>
    ///验证码表
    ///</summary>
    [SugarTable("Code")]
    public partial class Code
    {
           public Code(){


           }
           /// <summary>
           /// Desc:ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int id {get;set;}

           /// <summary>
           /// Desc:类型
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int type {get;set;}

           /// <summary>
           /// Desc:验证码邮箱
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string emali {get;set;}

           /// <summary>
           /// Desc:验证码手机
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string phone {get;set;}

           /// <summary>
           /// Desc:验证码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string code {get;set;}

           /// <summary>
           /// Desc:时间
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime time {get;set;}

           /// <summary>
           /// Desc:次数
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int count {get;set;}

    }
}
