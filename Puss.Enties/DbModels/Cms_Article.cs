using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Sugar.Enties
{
    ///<summary>
    ///CMS文章表
    ///</summary>
    [SugarTable("Cms_Article")]
    public partial class Cms_Article
    {
           public Cms_Article(){


           }
           /// <summary>
           /// Desc:ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsIdentity=true)]
           public int id {get;set;}

           /// <summary>
           /// Desc:栏目ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int catid {get;set;}

           /// <summary>
           /// Desc:用户ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int userid {get;set;}

           /// <summary>
           /// Desc:用户名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string username {get;set;}

           /// <summary>
           /// Desc:标题
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string title {get;set;}

           /// <summary>
           /// Desc:关键字
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string keywords {get;set;}

           /// <summary>
           /// Desc:描述
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string description {get;set;}

           /// <summary>
           /// Desc:内容
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string content {get;set;}

           /// <summary>
           /// Desc:thumb
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string thumb {get;set;}

           /// <summary>
           /// Desc:排序
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int listorder {get;set;}

           /// <summary>
           /// Desc:URL地址
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string url {get;set;}

           /// <summary>
           /// Desc:点击次数
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int hits {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int createtime {get;set;}

           /// <summary>
           /// Desc:更新时间
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int updatetime {get;set;}

           /// <summary>
           /// Desc:语言
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? lang {get;set;}

    }
}
