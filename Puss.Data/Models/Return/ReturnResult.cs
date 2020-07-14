using Puss.Data.Enum;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Puss.Data.Models
{
    /// <summary>
    /// 返回模型
    /// </summary>
    public class ReturnResult
    {
        /// <summary>
        /// 返回状态码，与 HttpStatusCode 一致
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 普通
        /// </summary>
        public ReturnResult()
        {
            Status = (int)ReturnResultStatus.ServerError;
        }

        /// <summary>
        /// 状态
        /// </summary>
        /// <param name="Status">状态</param>
        public ReturnResult(ReturnResultStatus Status)
            :this()
        {
            this.Status = (int)Status;
        }

        /// <summary>
        /// 消息
        /// </summary>
        /// <param name="Message">消息</param>
        public ReturnResult(string Message)
            : this()
        {
            this.Message = Message;
        }

        /// <summary>
        /// 状态 + 消息
        /// </summary>
        /// <param name="Status">状态</param>
        /// <param name="Message">消息</param>
        public ReturnResult(ReturnResultStatus Status, string Message)
            : this(Status)
        {
            this.Message = Message;
        }

        /// <summary>
        /// 传入委托实现返回不同结果
        /// </summary>
        /// <param name="func"></param>
        public static ReturnResult ResultCalculation(Func<bool> func)
        {
            bool result = func();
            return (result ? new ReturnResult(ReturnResultStatus.Succeed) : new ReturnResult());
        }

        /// <summary>
        /// 传入异步委托实现返回不同结果
        /// </summary>
        /// <param name="func"></param>
        public static async Task<ReturnResult> ResultCalculation(Func<Task<bool>> func)
        {
            bool result = await func();
            return (result ? new ReturnResult(ReturnResultStatus.Succeed) : new ReturnResult());
        }
    }

    /// <summary>
    /// 返回模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ReturnResult<T> : ReturnResult
    {
        /// <summary>
        /// 返回数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 普通
        /// </summary>
        public ReturnResult()
        {
            Status = (int)ReturnResultStatus.ServerError;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="Status">状态</param>
        public ReturnResult(ReturnResultStatus Status)
            : this()
        {
            this.Status = (int)Status;
        }

        /// <summary>
        /// 状态 + 数据
        /// </summary>
        /// <param name="Status">状态</param>
        /// <param name="Data">数据</param>
        public ReturnResult(ReturnResultStatus Status, T Data)
            : this(Status)
        {
            this.Data = Data;
        }

        /// <summary>
        /// 状态 + 数据
        /// </summary>
        /// <param name="Status">状态</param>
        /// <param name="Data">数据</param>
        public ReturnResult(ReturnResultStatus Status, T Data,string Message)
            : this(Status, Data)
        {
            this.Message = Message;
        }
    }
}
