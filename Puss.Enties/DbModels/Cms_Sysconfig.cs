using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Sugar.Enties
{
    ///<summary>
    ///Cms_Sysconfig
    ///</summary>
    [SugarTable("Cms_Sysconfig")]
    public partial class Cms_Sysconfig
    {
           public Cms_Sysconfig(){


           }
           /// <summary>
           /// Desc:ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

           /// <summary>
           /// Desc:地址
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Address {get;set;}

           /// <summary>
           /// Desc:电话
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Tel {get;set;}

           /// <summary>
           /// Desc:备案号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Record {get;set;}

           /// <summary>
           /// Desc:网站标题
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Title {get;set;}

           /// <summary>
           /// Desc:网站描述
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Description {get;set;}

           /// <summary>
           /// Desc:网站关键字
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Keywords {get;set;}

           /// <summary>
           /// Desc:Facebook
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Facebook {get;set;}

           /// <summary>
           /// Desc:网站图标
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Icon {get;set;}

           /// <summary>
           /// Desc:网站二维码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string QR_code {get;set;}

           /// <summary>
           /// Desc:邮箱配置发件人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Mail_From {get;set;}

           /// <summary>
           /// Desc:邮箱配置地址
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Mail_Host {get;set;}

           /// <summary>
           /// Desc:授权码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Mail_Code {get;set;}

    }
}
