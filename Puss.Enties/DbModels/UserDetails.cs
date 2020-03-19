using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Sugar.Enties
{
    ///<summary>
    ///用户详情表
    ///</summary>
    [SugarTable("UserDetails")]
    public partial class UserDetails
    {
           public UserDetails(){


           }
           /// <summary>
           /// Desc:ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int id {get;set;}

           /// <summary>
           /// Desc:用户ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int UID {get;set;}

           /// <summary>
           /// Desc:Msn
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Msn {get;set;}

           /// <summary>
           /// Desc:电话
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Tel {get;set;}

           /// <summary>
           /// Desc:生日
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? Birthday {get;set;}

           /// <summary>
           /// Desc:情感状况
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? Emotional {get;set;}

           /// <summary>
           /// Desc:兴趣
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Interest {get;set;}

           /// <summary>
           /// Desc:个人简介
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Describe {get;set;}

           /// <summary>
           /// Desc:个人网站
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Website {get;set;}

           /// <summary>
           /// Desc:省、自治区
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string province {get;set;}

           /// <summary>
           /// Desc:市
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string city {get;set;}

           /// <summary>
           /// Desc:县区
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string district {get;set;}

           /// <summary>
           /// Desc:详细地址
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string address {get;set;}

    }
}
