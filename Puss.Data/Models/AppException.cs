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
        /// 错误码
        /// </summary>
        public string ErrorCode { get; set; }

        /// <summary>
        /// 级别
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// App异常
        /// </summary>
        /// <param name="level">级别</param>
        /// <param name="errorStatus">错误状态</param>
        /// <param name="errorCode">错误码</param>
        /// <param name="message">消息</param>
        /// <param name="exception">异常</param>
        public AppException(int level, int errorStatus, string errorCode, string message, Exception exception)
            : base(message, exception)
        {
            this.ErrorStatus = errorStatus;
            this.Level = level;
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// App异常
        /// </summary>
        /// <param name="errorStatus">错误状态</param>
        /// <param name="errorCode">错误码</param>
        /// <param name="message">消息</param>
        public AppException(int errorStatus, string errorCode, string message)
            : this(0, errorStatus, errorCode, message, null)
        {
        }

        /// <summary>
        /// App异常
        /// </summary>
        /// <param name="errorCode">错误码</param>
        /// <param name="message">消息</param>
        public AppException(string errorCode, string message)
            : this((int)ReturnResultStatus.BLLError, errorCode, message)
        {
        }

        /// <summary>
        /// App异常
        /// </summary>
        /// <param name="message">消息</param>
        public AppException(string message)
            : this(string.Empty, message)
        {
        }

        /// <summary>
        /// App异常
        /// </summary>
        /// <param name="e">异常</param>
        public AppException(Exception e)
            : base(e.Message, e)
        {
        }
    }
}
