using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Puss.Enties
{
    ///<summary>
    ///导演和演员
    ///</summary>
    [SugarTable("Movie_Member")]
    public partial class Movie_Member
    {
           public Movie_Member(){


           }
           /// <summary>
           /// Desc:id
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public int id {get;set;}

           /// <summary>
           /// Desc:中文名字
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string sc_name {get;set;}

           /// <summary>
           /// Desc:英文名字
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string en_name {get;set;}

           /// <summary>
           /// Desc:角色名字
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string act_name {get;set;}

           /// <summary>
           /// Desc:头像地址
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string avatar {get;set;}

           /// <summary>
           /// Desc:枚举：1导演、2演员
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? type {get;set;}

           /// <summary>
           /// Desc:影片id
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? filmId {get;set;}

    }
}
