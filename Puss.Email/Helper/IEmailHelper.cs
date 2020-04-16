using Puss.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Puss.Email
{
    public interface IEmailHelper
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="Addressee">收件人邮箱</param>
        /// <param name="Title">邮件标题</param>
        /// <param name="Body">邮件正文</param>
        /// <param name="Mail_From">发件人邮箱</param>
        /// <param name="Mail_Code">授权码</param>
        /// <param name="Mail_Host">发件主机，默认QQ：smtp.qq.com</param>
        /// <returns></returns>
        bool MailSending(string Addressee, string Title, string Body, string Mail_From, string Mail_Code, string Mail_Host = "smtp.qq.com");
    }
}
