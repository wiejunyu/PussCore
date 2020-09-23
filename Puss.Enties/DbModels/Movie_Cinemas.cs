using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Puss.Enties
{
    ///<summary>
    ///影院
    ///</summary>
    [SugarTable("Movie_Cinemas")]
    public partial class Movie_Cinemas
    {
           public Movie_Cinemas(){


           }
           /// <summary>
           /// Desc:影院ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public int cinemaId {get;set;}

           /// <summary>
           /// Desc:城市ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int cityId {get;set;}

           /// <summary>
           /// Desc:影院名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string cinemaName {get;set;}

           /// <summary>
           /// Desc:城市名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string cityName {get;set;}

           /// <summary>
           /// Desc:影院地址
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string cinemaAddr {get;set;}

           /// <summary>
           /// Desc:地区名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string areaName {get;set;}

           /// <summary>
           /// Desc:影院电话
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string tel {get;set;}

           /// <summary>
           /// Desc:影院专资编码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string std_cinema_id {get;set;}

           /// <summary>
           /// Desc:区域ID（县区）
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string regionid {get;set;}

           /// <summary>
           /// Desc:经度
           /// Default:
           /// Nullable:True
           /// </summary>           
           public double? longitude {get;set;}

           /// <summary>
           /// Desc:纬度
           /// Default:
           /// Nullable:True
           /// </summary>           
           public double? latitude {get;set;}

    }
}
