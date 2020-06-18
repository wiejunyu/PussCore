using Puss.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;

namespace Puss.Workflow
{
    public class SetUserWorkflow : IWorkflow<UserWorkflowModels>
    {
        public string Id => "SetUserWorkflow";

        public int Version => 1;

        public void Build(IWorkflowBuilder<UserWorkflowModels> builder)
        {
            builder
                .StartWith<Apply>()
                .Then<Audit>()
                .If(x => !x.AuditStatus).Do(x => x.StartWith<Audit>()).If(x => x.AuditStatus).Do(x => x.StartWith<Pass>());
        }
    }
}
