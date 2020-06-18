using Puss.Data.Models;
using Puss.Redis;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace Puss.Workflow
{
    public class Apply : StepBodyAsync
    {
        private readonly IRedisService RedisService;

        public Apply(IRedisService RedisService)
        {
            this.RedisService = RedisService;
        }

        /// <summary>
        /// 审核用户
        /// </summary>
        public string UserName { get; set; }

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            UserWorkflowModels user = new UserWorkflowModels() 
            {
                UserName = UserName
            };
            await RedisService.SetAsync(CommentConfig.Workflow_ApplySetUser + user.UserName, user);
            return ExecutionResult.Next();
        }
    }

    public class Audit : StepBodyAsync
    {
        private readonly IRedisService RedisService;

        public Audit(IRedisService RedisService)
        {
            this.RedisService = RedisService;
        }

        /// <summary>
        /// 审核用户
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

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            var user = await RedisService.GetAsync<UserWorkflowModels>(CommentConfig.Workflow_ApplySetUser + UserName,() => null);
            user.AuditUserID = AuditUserID;
            user.AuditStatus = AuditStatus;
            await RedisService.SetAsync(CommentConfig.Workflow_ApplySetUser + user.UserName, user);
            return ExecutionResult.Next();
        }
    }

    public class Pass : StepBodyAsync
    {
        private readonly IRedisService RedisService;

        public Pass(IRedisService RedisService)
        {
            this.RedisService = RedisService;
        }

        /// <summary>
        /// 审核用户
        /// </summary>
        public string UserName { get; set; }

        public override async Task<ExecutionResult> RunAsync(IStepExecutionContext context)
        {
            await RedisService.RemoveAsync(CommentConfig.Workflow_ApplySetUser + UserName);
            return ExecutionResult.Next();
        }
    }
}
