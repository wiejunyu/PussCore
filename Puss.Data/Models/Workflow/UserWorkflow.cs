using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puss.Data.Models
{
    /// <summary>
    /// 用户工作流模型
    /// </summary>
    public class UserWorkflowModels
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 审核人员
        /// </summary>
        public int AuditUserID { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public bool AuditStatus { get; set; }
    }
}
