using Puss.Data.Models;
using Puss.RabbitMQ;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using Puss.Data.Enum;

namespace Puss.Attendance
{
    public class AttendanceSocket
    {
        #region 业务
        /// <summary>
        /// 心跳检测
        /// </summary>
        /// <param name="socket"></param>
        /// <returns>成功返回true</returns>
        public static async Task<bool> HeartbeatDetection(Socket socket)
        {
            try
            {
                #region 发送
                string sBody = FuncNo.CONNECT_STATUS + Basis.Serial_No;
                //长度
                sBody = (sBody.Length + 4).ToString("0000") + sBody;
                var Result = Send(socket, Encoding.Default.GetBytes(sBody));
                if (Result == null) throw new AppException("返回结果为空");
                #endregion

                #region 解析
                //获取完整包
                string sResult = Encoding.Default.GetString(ByteGetBody(Result));
                #region 头
                //包长度
                int Length = 0;
                string sLength = sResult.Substring(Length, 4);
                Length += 4;
                //功能号
                string sFunc_no = sResult.Substring(Length, 2);
                Length += 2;
                //序列号
                string sSerial_no = sResult.Substring(Length, 4);
                if (sFunc_no == FuncNo.ABT_STATUS)
                {
                    sResult = Encoding.Default.GetString(await ReStatusInquire(socket));
                    Length = 0;
                    sLength = sResult.Substring(Length, 4);
                    Length += 4;
                    //功能号
                    sFunc_no = sResult.Substring(Length, 2);
                    Length += 2;
                    //序列号
                    sSerial_no = sResult.Substring(Length, 4);
                }
                Length += 4;
                #endregion
                #endregion
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 公话认证
        /// </summary>
        /// <param name="socket"></param>
        /// <returns>成功返回true</returns>
        public static async Task<bool> Authentication(Socket socket)
        {
            return await Task.Run(() =>
            {
                try
                {
                    #region 发送
                    //发送内容 = 功能号 + 序列号 + 公话ID + 保留
                    string sBody = FuncNo.PHONE_AUTHEN + Basis.Serial_No + Supplement(Basis.Device_Id, 18) + Supplement("", 8);
                    sBody = (sBody.Length + 4).ToString("0000") + sBody;

                    byte[] Result = Send(socket, Encoding.Default.GetBytes(sBody));
                    if (Result == null) throw new AppException("返回结果为空");
                    #endregion

                    #region 解析
                    //获取完整包
                    string sResult = Encoding.Default.GetString(ByteGetBody(Result));

                    #region 头
                    //包长度
                    int Length = 0;
                    string sLength = sResult.Substring(Length, 4);
                    Length += 4;
                    //功能号
                    string sFunc_no = sResult.Substring(Length, 2);
                    Length += 2;
                    //序列号
                    string sSerial_no = sResult.Substring(Length, 4);
                    Length += 4;
                    #endregion

                    #region 包内容
                    //认证结果
                    string sValid_flag = sResult.Substring(Length, 1);
                    #endregion
                    #endregion
                    return sValid_flag.ToLower() == "1";
                }
                catch 
                {
                    return false;
                }
            });
        }

        /// <summary>
        /// 返回server端查询的公话状态
        /// </summary>
        /// <param name="socket"></param>
        /// <returns></returns>
        public static async Task<byte[]> ReStatusInquire(Socket socket)
        {
            return await Task.Run(() =>
            {
                try
                {
                    //发送内容 = 功能号 + 序列号 + 公话ID + 版本信息 + 预留监控信息
                    string sBody = FuncNo.ABT_STATUS + Basis.Serial_No + Supplement(Basis.Device_Id, 18) + Basis.VersionInfo + Supplement(Basis.MinitorInfo, 72);
                    sBody = (sBody.Length + 4).ToString("0000") + sBody;
                    byte[] Result = Send(socket, Encoding.Default.GetBytes(sBody));
                    if (Result == null) throw new AppException("返回结果为空");
                    return Result;
                }
                catch 
                {
                    return null;
                }
            });
        }

        /// <summary>
        /// 发送学生进校离校记录
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="sCardId">学生卡号</param>
        /// <param name="sTemperature">温度</param>
        /// <param name="iOptType">进出校状态(枚举)</param>
        /// <returns>成功返回true</returns>
        public static async Task<bool> SendAttendance(Socket socket,string sCardId,string sTemperature,int iOptType)
        {
            try
            {
                #region 发送
                //发送内容 = 功能号 + 序列号 + 公话ID + 学生卡号 + 时间 + 进入校状态
                string sBody = FuncNo.STDT_SCHOOL_RECS + Basis.Serial_No + Supplement(Basis.Device_Id, 18) + Supplement(sCardId, 18) + Supplement(sTemperature, 18) + Supplement(DateTime.Now.ToString("yyyyMMddhhmmss"), 14) + iOptType.ToString();
                sBody = (sBody.Length + 4).ToString("0000") + sBody;
                var Result = Send(socket, Encoding.Default.GetBytes(sBody));
                if (Result == null) throw new AppException("返回结果为空");
                #endregion

                #region 解析
                //获取完整包
                string sResult = Encoding.Default.GetString(ByteGetBody(Result));

                #region 头
                //包长度
                int Length = 0;
                string sLength = sResult.Substring(Length, 4);
                Length += 4;
                //功能号
                string sFunc_no = sResult.Substring(Length, 2);
                //如果需要获取公话状态，先发送公话状态在获取真实结果
                if (sFunc_no == FuncNo.ABT_STATUS)
                {
                    //获取完整包
                    sResult = Encoding.Default.GetString(ByteGetBody(await ReStatusInquire(socket)));
                    //包长度
                    Length = 0;
                    sLength = sResult.Substring(Length, 4);
                    Length += 4;
                    //功能号
                    sFunc_no = sResult.Substring(Length, 2);
                }
                Length += 2;
                //序列号
                string sSerial_no = sResult.Substring(Length, 4);
                Length += 4;
                #endregion

                #region 包内容
                //学生卡是否有效
                string sValid_flag = sResult.Substring(Length, 1);
                Length += 1;
                #endregion
                #endregion

                return sValid_flag == "1";
            }
            catch 
            {
                return false;
            }
        }
        #endregion

        #region socket操作
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private static byte[] Send(Socket socket, byte[] data)
        {
            if (!socket.Connected) throw new AppException("没有连接上");
            socket.Send(data);
            return Receive(socket, 1000 * 2); //1*2 seconds timeout.
        }

        /// <summary>
        /// 销毁Socket对象
        /// </summary>
        /// <param name="socket"></param>
        public static void DestroySocket(Socket socket)
        {
            if (socket.Connected)
            {
                socket.Shutdown(SocketShutdown.Both);
            }
            socket.Close();
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="socket"></param>
        /// <param name="timeout"></param>
        /// <returns></returns>
        private static byte[] Receive(Socket socket, int timeout)
        {
            try
            {
                socket.ReceiveTimeout = timeout;

                //定义接收数据的缓存
                byte[] body = new byte[1024];
                //第一次接收的实际数据 flag
                int flag = socket.Receive(body, 0, body.Length, SocketFlags.None);
                //如果没有接收到定长的数据，循环接收
                while (flag <= 0)
                {
                    flag += socket.Receive(body, flag, body.Length - flag, SocketFlags.None);
                }
                return body;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 扩展
        /// <summary>
        /// 获取完整包
        /// </summary>
        /// <param name="Body"></param>
        /// <returns></returns>
        private static byte[] ByteGetBody(byte[] Body)
        {
            string sLength = Encoding.Default.GetString(Body.Skip(0).Take(4).ToArray());
            return Body.Skip(0).Take(int.Parse(sLength)).ToArray();
        }

        /// <summary>
        /// 自动补位
        /// </summary>
        /// <param name="sInput">需要补位的字符串</param>
        /// <param name="iPlaces">补位位数，必须大于字符串位数</param>
        /// <returns>补位后的字符串</returns>
        private static string Supplement(string sInput, int iPlaces)
        {
            if (iPlaces <= sInput.Length) return sInput;
            iPlaces -= sInput.Length;
            for (int i = 0; i < iPlaces; i++)
            {
                sInput += " ";
            }
            return sInput;
        }
        #endregion
    }
}
