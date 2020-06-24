using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WorkflowCore.Interface;
using WorkflowCore.Models;

namespace Puss.Workflow.User
{
    public class UserTaskStep : WorkflowStep<UserTask>
    {

        public Dictionary<bool, string> Options { get; set; } = new Dictionary<bool, string>();

        public List<EscalateStep> Escalations { get; set; } = new List<EscalateStep>();

        public override IStepBody ConstructBody(IServiceProvider serviceProvider)
        {
            return new UserTask(Options, Escalations);
        }
    }
}
