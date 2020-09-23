using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Puss.Enties
{
    ///<summary>
    ///场次
    ///</summary>
    [SugarTable("Movie_Shows")]
    public partial class Movie_Shows
    {
           public Movie_Shows(){


           }
           /// <summary>
           /// Desc:ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? cinemaId {get;set;}

           /// <summary>
           /// Desc:厅名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string hallName {get;set;}

           /// <summary>
           /// Desc:电影ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? filmId {get;set;}

           /// <summary>
           /// Desc:电影名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string filmName {get;set;}

           /// <summary>
           /// Desc:场次ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public int showId {get;set;}

           /// <summary>
           /// Desc:播放时长
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? duration {get;set;}

           /// <summary>
           /// Desc:开演时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? showTime {get;set;}

           /// <summary>
           /// Desc:停售时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? stopSellTime {get;set;}

           /// <summary>
           /// Desc:场次类型国语 2D
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string showVersionType {get;set;}

           /// <summary>
           /// Desc:结算价格
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? settlePrice {get;set;}

           /// <summary>
           /// Desc:市场价
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? price {get;set;}

           /// <summary>
           /// Desc:最低价
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? minPrice {get;set;}

           /// <summary>
           /// Desc:语言类型  中文 原版  英文
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string language {get;set;}

           /// <summary>
           /// Desc:影厅类型  2D 3D
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string planType {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime createTime {get;set;}

    }
}
