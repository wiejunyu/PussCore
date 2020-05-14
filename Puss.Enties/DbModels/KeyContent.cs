using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Puss.Enties
{
    ///<summary>
    ///密匙内容
    ///</summary>
    [SugarTable("KeyContent")]
    public partial class KeyContent
    {
           public KeyContent(){


           }
           /// <summary>
           /// Desc:ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int ID {get;set;}

           /// <summary>
           /// Desc:创建用户ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int CreateUserID {get;set;}

           /// <summary>
           /// Desc:栏目ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int SectionID {get;set;}

           /// <summary>
           /// Desc:密匙名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Name {get;set;}

           /// <summary>
           /// Desc:用户/账号本文
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string UserText {get;set;}

           /// <summary>
           /// Desc:密码本文
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string PasswordText {get;set;}

           /// <summary>
           /// Desc:网址本文
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string UrlText {get;set;}

           /// <summary>
           /// Desc:手机本文
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string IphoneText {get;set;}

           /// <summary>
           /// Desc:邮箱文本
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MailText {get;set;}

           /// <summary>
           /// Desc:其他本文
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string OtherText {get;set;}

           /// <summary>
           /// Desc:备注
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Remarks {get;set;}

           /// <summary>
           /// Desc:是否启用
           /// Default:
           /// Nullable:False
           /// </summary>           
           public bool IsStart {get;set;}

    }
}
