using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Puss.Enties
{
    ///<summary>
    ///城市列表
    ///</summary>
    [SugarTable("Movie_City")]
    public partial class Movie_City
    {
           public Movie_City(){


           }
           /// <summary>
           /// Desc:ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int id {get;set;}

           /// <summary>
           /// Desc:城市ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string cityId {get;set;}

           /// <summary>
           /// Desc:城市名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string cityName {get;set;}

           /// <summary>
           /// Desc:城市首字母
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string firstletter {get;set;}

           /// <summary>
           /// Desc:是否为热门城市,0-非热门，1-热门
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int ishot {get;set;}

    }
}
