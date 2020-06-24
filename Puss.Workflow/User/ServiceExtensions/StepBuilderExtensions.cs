using System;
using System.Linq.Expressions;
using WorkflowCore.Interface;
using WorkflowCore.Models;
using WorkflowCore.Services;

namespace Puss.Workflow.User
{
    public static class StepBuilderExtensions
    {
        [Obsolete]
        public static IStepBuilder<TData, UserStep> UserStep<TData, TStepBody>(this IStepBuilder<TData, TStepBody> builder, string userPrompt, Expression<Func<TData, string>> assigner, Action<IStepBuilder<TData, UserStep>> stepSetup = null)
            where TStepBody : IStepBody
        {
            var newStep = new UserStepContainer()
            {
                Principal = assigner,
                UserPrompt = userPrompt
            };           
            builder.WorkflowBuilder.AddStep(newStep);
            var stepBuilder = new StepBuilder<TData, UserStep>(builder.WorkflowBuilder, newStep);

            if (stepSetup != null)
                stepSetup.Invoke(stepBuilder);
            newStep.Name ??= typeof(UserStepContainer).Name;

            builder.Step.Outcomes.Add(new ValueOutcome() { NextStep = newStep.Id });
            return stepBuilder;
        }

        [Obsolete]
        public static IStepBuilder<TData, UserStep> UserStep<TData>(this IStepOutcomeBuilder<TData> builder, string userPrompt, Expression<Func<TData, string>> assigner, Action<IStepBuilder<TData, UserStep>> stepSetup = null)
        {
            var newStep = new UserStepContainer() 
            {
                Principal = assigner,
                UserPrompt = userPrompt,
            };
            builder.WorkflowBuilder.AddStep(newStep);
            var stepBuilder = new StepBuilder<TData, UserStep>(builder.WorkflowBuilder, newStep);

            if (stepSetup != null)
                stepSetup.Invoke(stepBuilder);
            newStep.Name ??= typeof(UserStepContainer).Name;

            builder.Outcome.NextStep = newStep.Id;
            return stepBuilder;
        }

        public static IUserTaskBuilder<TData> UserTask<TData, TStepBody>(this IStepBuilder<TData, TStepBody> builder, string userPrompt, Expression<Func<TData, string>> assigner, Action<IStepBuilder<TData, UserTask>> stepSetup = null)
            where TStepBody : IStepBody
        {
            var newStep = new UserTaskStep();
            builder.WorkflowBuilder.AddStep(newStep);
            var stepBuilder = new UserTaskBuilder<TData>(builder.WorkflowBuilder, newStep);
            stepBuilder.Input(step => step.AssignedPrincipal, assigner);
            stepBuilder.Input(step => step.Prompt, data => userPrompt);

            if (stepSetup != null)
                stepSetup.Invoke(stepBuilder);

            newStep.Name ??= typeof(UserTask).Name;
            builder.Step.Outcomes.Add(new ValueOutcome() { NextStep = newStep.Id });

            return stepBuilder;
        }
    }
}
