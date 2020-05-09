using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Puss.Attendance;
using Puss.Data.Enum;
using Puss.Data.Models;
using Puss.RabbitMQ;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Puss.Api.Controllers
{
    /// <summary>
    /// 考勤
    /// </summary>
    [AllowAnonymous]
    public class AttendanceSocketController : ApiBaseController
    {
        private readonly IRabbitMQPushService RabbitMQPushService;
        /// <summary>
        /// 考勤
        /// </summary>
        /// <param name="RabbitMQPushService"></param>
        public AttendanceSocketController(IRabbitMQPushService RabbitMQPushService) 
        {
            this.RabbitMQPushService = RabbitMQPushService;
        }

        /// <summary>
        /// 学生进校离校记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ReturnResult> SendAttendance()
        {
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse(Puss.Attendance.Connection.Host);
            IPEndPoint point = new IPEndPoint(ip, Puss.Attendance.Connection.Port);
            socket.Connect(point);
            
            await AttendanceSocket.Authentication(socket);
            await AttendanceSocket.SendAttendance(socket);
            return new ReturnResult(ReturnResultStatus.Succeed);
        }
    }
}