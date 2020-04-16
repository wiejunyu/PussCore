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
    public class EmailHelper: IEmailHelper
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
        public bool MailSending(string Addressee,string Title,string Body,string Mail_From,string Mail_Code,string Mail_Host = "smtp.qq.com")
        {
            try
            {
                //实例化一个发送邮件类。
                MailMessage mailMessage = new MailMessage() 
                {
                    //发件人邮箱地址，方法重载不同，可以根据需求自行选择。
                    From = new MailAddress(Mail_From),
                    //邮件标题。
                    Subject = Title,
                    //邮件内容。
                    Body = Body,
                };
                //收件人邮箱地址。
                mailMessage.To.Add(new MailAddress(Addressee));

                //实例化一个SmtpClient类。
                SmtpClient client = new SmtpClient() 
                {
                    //在这里我使用的是qq邮箱，所以是smtp.qq.com，如果你使用的是126邮箱，那么就是smtp.126.com。
                    Host = Mail_Host,
                    //使用安全加密连接。
                    EnableSsl = true,
                    //不和请求一块发送。
                    UseDefaultCredentials = false,
                    //验证发件人身份(发件人的邮箱，邮箱里的生成授权码);
                    Credentials = new NetworkCredential(Mail_From, Mail_Code),
                };
                //发送
                client.Send(mailMessage);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
