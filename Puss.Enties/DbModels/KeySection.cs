using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Puss.Enties
{
    ///<summary>
    ///密匙栏目
    ///</summary>
    [SugarTable("KeySection")]
    public partial class KeySection
    {
           public KeySection(){


           }
           /// <summary>
           /// Desc:ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int ID {get;set;}

           /// <summary>
           /// Desc:创建用户ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int CreateUserID {get;set;}

           /// <summary>
           /// Desc:栏目名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string Name {get;set;}

           /// <summary>
           /// Desc:备注
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Remarks {get;set;}

           /// <summary>
           /// Desc:栏目级别
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int Level {get;set;}

           /// <summary>
           /// Desc:父栏目ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public int? PID {get;set;}

    }
}
