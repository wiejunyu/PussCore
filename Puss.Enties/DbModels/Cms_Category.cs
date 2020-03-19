using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Sugar.Enties
{
    ///<summary>
    ///CMS栏目表
    ///</summary>
    [SugarTable("Cms_Category")]
    public partial class Cms_Category
    {
           public Cms_Category(){


           }
           /// <summary>
           /// Desc:ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int id {get;set;}

           /// <summary>
           /// Desc:栏目名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string catname {get;set;}

           /// <summary>
           /// Desc:父ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int parentid {get;set;}

           /// <summary>
           /// Desc:文章模型
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int moduleid {get;set;}

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
           /// Desc:排序
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int listorder {get;set;}

           /// <summary>
           /// Desc:点击次数
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int hits {get;set;}

           /// <summary>
           /// Desc:栏目图片
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string image {get;set;}

           /// <summary>
           /// Desc:URL路径
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string url {get;set;}

           /// <summary>
           /// Desc:语言
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? lang {get;set;}

           /// <summary>
           /// Desc:栏目目录
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string catdir {get;set;}

    }
}
