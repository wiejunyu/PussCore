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
    public class AttendanceTcpController : ApiBaseController
    {
        private readonly IRabbitMQPushService RabbitMQPushService;
        /// <summary>
        /// 考勤
        /// </summary>
        /// <param name="RabbitMQPushService"></param>
        public AttendanceTcpController(IRabbitMQPushService RabbitMQPushService) 
        {
            this.RabbitMQPushService = RabbitMQPushService;
        }

        /// <summary>
        /// 心跳检测
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ReturnResult> HeartbeatDetection()
        {
            TcpClient tcpClient = new TcpClient();
            IPAddress ip = IPAddress.Parse(Puss.Attendance.Connection.Host);
            tcpClient.Connect(ip,Puss.Attendance.Connection.Port);

            await AttendanceTcp.HeartbeatDetection(tcpClient);
            return new ReturnResult(ReturnResultStatus.Succeed);
        }

        /// <summary>
        /// 发送学生进校离校记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ReturnResult> SendAttendance()
        {
            #region 功能号
            byte[] func_no = System.Text.Encoding.Default.GetBytes("28");
            #endregion
            #region 序列号
            byte[] serial_no = System.BitConverter.GetBytes(0000);
            #endregion
            #region 包内容
            //公话ID(device_id)
            byte[] device_id = System.Text.Encoding.Default.GetBytes("117000169649054977");
            //学生卡号(card_id)
            string sCard_id = "KQK001507";
            int iLength = (18 - sCard_id.Length);
            for (int i = 0; i < iLength; i++)
            {
                sCard_id += " ";
            }
            byte[] card_id = System.Text.Encoding.Default.GetBytes(sCard_id);
            //温度(temperature)
            string sTemperature = "37.3";
            iLength = (18 - sCard_id.Length);
            for (int i = 0; i < iLength; i++)
            {
                sTemperature += " ";
            }
            byte[] temperature = System.Text.Encoding.Default.GetBytes(sTemperature);
            //签到时间(start_time)
            byte[] start_time = System.Text.Encoding.Default.GetBytes(DateTime.Now.ToString("yyyyMMddhhmmss"));
            //0-进校 1-离校(Opt_type)
            byte[] Opt_type = System.BitConverter.GetBytes(0);
            #endregion
            #region 包长度
            int Length = func_no.Length + serial_no.Length + device_id.Length + card_id.Length + temperature.Length + temperature.Length + start_time.Length + Opt_type.Length + 4;
            byte[] PacketLength = System.BitConverter.GetBytes(Length);
            #endregion
            byte[] resArr = new byte[Length];
            Length = 0;

            PacketLength.CopyTo(resArr, 0);
            Length += PacketLength.Length;
            func_no.CopyTo(resArr, PacketLength.Length);
            Length += func_no.Length;
            serial_no.CopyTo(resArr, Length);
            Length += serial_no.Length;
            device_id.CopyTo(resArr, Length);
            Length += device_id.Length;
            card_id.CopyTo(resArr, Length);
            Length += card_id.Length;
            temperature.CopyTo(resArr, Length);
            Length += temperature.Length;
            temperature.CopyTo(resArr, Length);
            Length += temperature.Length;
            start_time.CopyTo(resArr, Length);
            Length += start_time.Length;
            Opt_type.CopyTo(resArr, Length);
            //bool bHeartbeatDetection = await AttendanceService.HeartbeatDetection();
            //byte[] result = Send("211.138.251.205", 6186, resArr);
            return new ReturnResult(ReturnResultStatus.Succeed);
        }
    }
}