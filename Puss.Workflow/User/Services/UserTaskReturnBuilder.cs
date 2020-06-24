﻿using System;
using System.Collections.Generic;
using System.Text;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Primitives;

namespace Puss.Workflow.User
{
    public class UserTaskReturnBuilder<TData> : IUserTaskReturnBuilder<TData>
    {
        private readonly IUserTaskBuilder<TData> _referenceBuilder;

        public IWorkflowBuilder<TData> WorkflowBuilder { get; private set; }

        public WorkflowStep<When> Step { get; set; }

        public UserTaskReturnBuilder(IWorkflowBuilder<TData> workflowBuilder, WorkflowStep<When> step, IUserTaskBuilder<TData> referenceBuilder)
        {
            WorkflowBuilder = workflowBuilder;
            Step = step;
            _referenceBuilder = referenceBuilder;
        }

        public IUserTaskBuilder<TData> Do(Action<IWorkflowBuilder<TData>> builder)
        {
            builder.Invoke(WorkflowBuilder);
            Step.Children.Add(Step.Id + 1); //TODO: make more elegant

            return _referenceBuilder;
        }
    }
}
