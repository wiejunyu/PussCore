using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Puss.Api.Aop;
using Puss.Api.Manager;
using Puss.Data.Enum;
using Puss.Data.Models;
using Puss.Workflow;
using System.Threading.Tasks;
using WorkflowCore.Interface;

namespace Puss.Api.Controllers
{
    /// <summary>
    /// 工作流
    /// </summary>
    public class WorkflowController : ApiBaseController
    {
        private readonly IWorkflowHost WorkflowHost;

        /// <summary>
        /// 工作流
        /// </summary>
        /// <param name="WorkflowHost"></param>
        public WorkflowController(
            IWorkflowHost WorkflowHost
            )
        {
            this.WorkflowHost = WorkflowHost;
        }

        /// <summary>
        /// 开始工作流
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<ReturnResult> StartWorkflow()
        {
            WorkflowHost.Start();
            WorkflowHost.RegisterWorkflow<SetUserWorkflow, UserWorkflowModels>();
            string re = await WorkflowHost.StartWorkflow("SetUserWorkflow",new UserWorkflowModels());
            return new ReturnResult(re);
        }
    }
}