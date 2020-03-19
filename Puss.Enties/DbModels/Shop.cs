using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Sugar.Enties
{
    ///<summary>
    ///商品表
    ///</summary>
    [SugarTable("Shop")]
    public partial class Shop
    {
           public Shop(){


           }
           /// <summary>
           /// Desc:ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int id {get;set;}

           /// <summary>
           /// Desc:商品名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Name {get;set;}

           /// <summary>
           /// Desc:商品栏目ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int Catid {get;set;}

           /// <summary>
           /// Desc:上架时间
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime Uptime {get;set;}

           /// <summary>
           /// Desc:计量单位
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Units {get;set;}

           /// <summary>
           /// Desc:品牌
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Brand {get;set;}

           /// <summary>
           /// Desc:优惠价
           /// Default:
           /// Nullable:False
           /// </summary>           
           public decimal Trueprice {get;set;}

           /// <summary>
           /// Desc:市场价
           /// Default:
           /// Nullable:False
           /// </summary>           
           public decimal Price {get;set;}

           /// <summary>
           /// Desc:详细介绍
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Body {get;set;}

           /// <summary>
           /// Desc:型号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Model {get;set;}

           /// <summary>
           /// Desc:行业
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Vocation {get;set;}

           /// <summary>
           /// Desc:点击次数
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int Click {get;set;}

           /// <summary>
           /// Desc:购买次数
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int Buy {get;set;}

           /// <summary>
           /// Desc:推荐
           /// Default:
           /// Nullable:False
           /// </summary>           
           public bool Recommend {get;set;}

           /// <summary>
           /// Desc:缩略图
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Image {get;set;}

           /// <summary>
           /// Desc:高清图
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string HighImage {get;set;}

           /// <summary>
           /// Desc:备注
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Remarks {get;set;}

           /// <summary>
           /// Desc:参数
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Parameter {get;set;}

           /// <summary>
           /// Desc:品牌证书
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Certificate {get;set;}

    }
}
