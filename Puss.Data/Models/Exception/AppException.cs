using Puss.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Puss.Data.Models
{
    /// <summary>
    /// App异常
    /// </summary>
    public class AppException : Exception
    {
        /// <summary>
        /// 错误状态
        /// </summary>
        public int ErrorStatus { get; set; }

        /// <summary>
        /// App异常
        /// </summary>
        /// <param name="errorStatus">错误状态</param>
        /// <param name="message">消息</param>
        /// <param name="exception">异常</param>
        /// <param name="fun">委托</param>
        public AppException(ReturnResultStatus errorStatus, string message, Exception exception, Func<bool> fun = null)
            : base(message, exception)
        {
            this.ErrorStatus = (int)errorStatus;
            fun?.Invoke();
        }

        /// <summary>
        /// App异常
        /// </summary>
        /// <param name="errorStatus">错误状态</param>
        /// <param name="message">消息</param>
        /// <param name="fun">委托</param>
        public AppException(ReturnResultStatus errorStatus, string message, Func<bool> fun = null)
            : this(errorStatus, message, null, fun)
        {
        }

        /// <summary>
        /// App异常
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="fun">委托</param>
        public AppException(string message, Func<bool> fun = null)
            : this(ReturnResultStatus.BLLError, message, fun)
        {
        }

        /// <summary>
        /// App异常
        /// </summary>
        /// <param name="e">异常</param>
        /// <param name="fun">委托</param>
        public AppException(Exception e, Func<bool> fun = null)
            : base(e.Message, e)
        {
            fun?.Invoke();
        }
    }
}
