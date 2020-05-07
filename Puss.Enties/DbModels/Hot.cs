using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Puss.Enties
{
    ///<summary>
    ///商品热门表
    ///</summary>
    [SugarTable("Hot")]
    public partial class Hot
    {
           public Hot(){


           }
           /// <summary>
           /// Desc:ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int id {get;set;}

           /// <summary>
           /// Desc:热门图片
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Image {get;set;}

           /// <summary>
           /// Desc:商品ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int Sid {get;set;}

    }
}
