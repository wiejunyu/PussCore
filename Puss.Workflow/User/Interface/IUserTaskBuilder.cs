using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using WorkflowCore.Interface;

namespace Puss.Workflow.User
{
    public interface IUserTaskBuilder<TData> : IStepBuilder<TData, UserTask>
    {
        /// <summary>
        /// 向用户任务添加可选选项
        /// </summary>
        /// <param name="value">此选项的值</param>
        /// <param name="label">此选项的标签</param>
        /// <returns></returns>
        IUserTaskReturnBuilder<TData> WithOption(bool value, string label);

        /// <summary>
        /// Escalate this task to another user after a given period
        /// </summary>
        /// <param name="after">Period to wait before escalating</param>
        /// <param name="newUser">The user to escalate this task to</param>
        /// <param name="action"></param>
        /// <returns></returns>
        IUserTaskBuilder<TData> WithEscalation(Expression<Func<TData, TimeSpan>> after, Expression<Func<TData, string>> newUser, Action<IWorkflowBuilder<TData>> action = null);


    }
}
