using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Primitives;

namespace Puss.Workflow.User
{
    public interface IUserTaskReturnBuilder<TData>
    {
        IWorkflowBuilder<TData> WorkflowBuilder { get; }
        WorkflowStep<When> Step { get; set; }
        IUserTaskBuilder<TData> Do(Action<IWorkflowBuilder<TData>> builder);
    }
}
