using Puss.Data.Enum;
using System;

namespace Puss.Data.Models
{
    /// <summary>
    /// 返回模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ReturnResult<T>
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
        /// 返回数据
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 返回模型
        /// </summary>
        public ReturnResult()
        {
            Status = (int)ReturnResultStatus.ServerError;
        }

        /// <summary>
        /// 返回模型
        /// </summary>
        /// <param name="Data">数据</param>
        public ReturnResult(T Data)
        {
            Status = (int)ReturnResultStatus.ServerError;
            this.Data = Data;
        }

        /// <summary>
        /// 错误返回
        /// </summary>
        /// <param name="ex">异常</param>
        public void HandleException(Exception ex)
        {
            this.Status = (int)ReturnResultStatus.BLLError;
            this.Message = ex.Message;
        }

        /// <summary>
        /// 错误返回
        /// </summary>
        /// <param name="ex">异常</param>
        public void HandleException(AppException ex)
        {
            this.Status = ex.ErrorStatus;
            this.Message = ex.Message;
        }
    }
}
