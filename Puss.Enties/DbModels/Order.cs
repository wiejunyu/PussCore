using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Sugar.Enties
{
    ///<summary>
    ///订单表
    ///</summary>
    [SugarTable("Order")]
    public partial class Order
    {
           public Order(){


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
           /// Desc:订单号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string OrderNumber {get;set;}

           /// <summary>
           /// Desc:商品ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int Sid {get;set;}

           /// <summary>
           /// Desc:商品价格
           /// Default:
           /// Nullable:False
           /// </summary>           
           public decimal Price {get;set;}

           /// <summary>
           /// Desc:商品数量
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int Number {get;set;}

           /// <summary>
           /// Desc:运费
           /// Default:
           /// Nullable:False
           /// </summary>           
           public decimal Freight {get;set;}

           /// <summary>
           /// Desc:应付金额
           /// Default:
           /// Nullable:False
           /// </summary>           
           public decimal Money {get;set;}

           /// <summary>
           /// Desc:收货地址
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Address {get;set;}

           /// <summary>
           /// Desc:发票信息
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Invoice {get;set;}

           /// <summary>
           /// Desc:订单生成日期
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime Date {get;set;}

           /// <summary>
           /// Desc:支付状态
           /// Default:
           /// Nullable:False
           /// </summary>           
           public bool PayState {get;set;}

           /// <summary>
           /// Desc:支付方式：0货到付款
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int PayMethod {get;set;}

           /// <summary>
           /// Desc:物流状态，0未发货，1运输中，2已收货
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int LogisticsState {get;set;}

           /// <summary>
           /// Desc:物流信息
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Logistics {get;set;}

           /// <summary>
           /// Desc:备注
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Remarks {get;set;}

           /// <summary>
           /// Desc:购物车订单商品IDjson
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MultipleSid {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public bool? ShoppingCart {get;set;}

    }
}
