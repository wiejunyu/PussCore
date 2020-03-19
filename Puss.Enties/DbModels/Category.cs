using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Sugar.Enties
{
    ///<summary>
    ///栏目表
    ///</summary>
    [SugarTable("Category")]
    public partial class Category
    {
           public Category(){


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
           public string Name {get;set;}

           /// <summary>
           /// Desc:栏目等级
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int Rank {get;set;}

           /// <summary>
           /// Desc:栏目图片，空为无图栏目
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Picture {get;set;}

           /// <summary>
           /// Desc:URL路径
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Url {get;set;}

           /// <summary>
           /// Desc:父栏目ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int Parentid {get;set;}

           /// <summary>
           /// Desc:是否推荐
           /// Default:
           /// Nullable:False
           /// </summary>           
           public bool Recommend {get;set;}

           /// <summary>
           /// Desc:点击次数
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int Click {get;set;}

    }
}
