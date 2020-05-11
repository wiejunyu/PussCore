﻿using Hangfire;
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
            //socket创建
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPAddress ip = IPAddress.Parse(Puss.Attendance.Connection.Host);
            IPEndPoint point = new IPEndPoint(ip, Puss.Attendance.Connection.Port);
            socket.Connect(point);

            bool re = false;
            //先进行公话认证才能执行别的操作
            if (await AttendanceSocket.Authentication(socket)) 
            {
                //心跳检测保证socket不被断开
                re = await AttendanceSocket.HeartbeatDetection(socket);
                //发送学生进校离校记录
                re = await AttendanceSocket.SendAttendance(socket, Basis.Card_Id, "37.3", (int)OptType.In);
            }
            return ReturnResult.ResultCalculation(() => re);
        }
    }
}