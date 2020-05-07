using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace Puss.Enties
{
    ///<summary>
    ///角色按钮关联表
    ///</summary>
    [SugarTable("Cms_RoleMenu")]
    public partial class Cms_RoleMenu
    {
           public Cms_RoleMenu(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int ID {get;set;}

           /// <summary>
           /// Desc:按钮ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int MenuID {get;set;}

           /// <summary>
           /// Desc:角色ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           public int RoleID {get;set;}

    }
}
