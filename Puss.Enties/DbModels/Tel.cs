using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Puss.Enties
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Tel")]
    public partial class Tel
    {
           public Tel(){


           }
           /// <summary>
           /// Desc:id
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int id {get;set;}

           /// <summary>
           /// Desc:电话
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string tel {get;set;}

           /// <summary>
           /// Desc:状态
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int state {get;set;}

    }
}
