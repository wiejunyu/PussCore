using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Sugar.Enties
{
    ///<summary>
    ///购物车
    ///</summary>
    [SugarTable("ShoppingCart")]
    public partial class ShoppingCart
    {
           public ShoppingCart(){


           }
           /// <summary>
           /// Desc:ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int Id {get;set;}

           /// <summary>
           /// Desc:用户ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int UserId {get;set;}

           /// <summary>
           /// Desc:商品ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int ShopId {get;set;}

           /// <summary>
           /// Desc:数量
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int Number {get;set;}

           /// <summary>
           /// Desc:加入购物车时间
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime Date {get;set;}

    }
}
