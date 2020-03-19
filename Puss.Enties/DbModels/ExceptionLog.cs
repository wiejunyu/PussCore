using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Sugar.Enties
{
    ///<summary>
    ///系统异常记录
    ///</summary>
    [SugarTable("ExceptionLog")]
    public partial class ExceptionLog
    {
           public ExceptionLog(){


           }
           /// <summary>
           /// Desc:ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int ID {get;set;}

           /// <summary>
           /// Desc:Url
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Url {get;set;}

           /// <summary>
           /// Desc:Request
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Request {get;set;}

           /// <summary>
           /// Desc:Response
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Response {get;set;}

           /// <summary>
           /// Desc:1.系统异常  2.业务异常
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Exception_Type {get;set;}

           /// <summary>
           /// Desc:错误代码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Error_code {get;set;}

           /// <summary>
           /// Desc:状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Status {get;set;}

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

    }
}
