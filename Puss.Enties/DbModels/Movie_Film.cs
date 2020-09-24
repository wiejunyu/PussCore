using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Puss.Enties
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Movie_Film")]
    public partial class Movie_Film
    {
           public Movie_Film(){


           }
           /// <summary>
           /// Desc:id
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public int filmId {get;set;}

           /// <summary>
           /// Desc:影片评分
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string grade {get;set;}

           /// <summary>
           /// Desc:中国大陆,中国香港
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string moviearea {get;set;}

           /// <summary>
           /// Desc:影片名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string name {get;set;}

           /// <summary>
           /// Desc:影片时长
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? duration {get;set;}

           /// <summary>
           /// Desc:影片上映日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? publishDate {get;set;}

           /// <summary>
           /// Desc:影片导演
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string director {get;set;}

           /// <summary>
           /// Desc:影片主演
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string cast {get;set;}

           /// <summary>
           /// Desc:影片简介
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string intro {get;set;}

           /// <summary>
           /// Desc:上映类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string versionTypes {get;set;}

           /// <summary>
           /// Desc:影片语言
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string language {get;set;}

           /// <summary>
           /// Desc:影片类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string filmTypes {get;set;}

           /// <summary>
           /// Desc:海报URL地址
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string pic {get;set;}

           /// <summary>
           /// Desc:剧情照URL地址
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string imgs {get;set;}

           /// <summary>
           /// Desc:想看人数
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? like {get;set;}

           /// <summary>
           /// Desc:专资办影片统一编码11位(12位编码去除第四位)
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string filmCode {get;set;}

    }
}
