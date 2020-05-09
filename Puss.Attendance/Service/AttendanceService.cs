using Puss.Data.Models;
using Puss.RabbitMQ;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Puss.Attendance
{
    public class AttendanceSocket
    {
        /// <summary>
        /// 心跳检测
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> HeartbeatDetection(Socket socket)
        {
            return await Task.Run(() =>
            {
                #region 功能号
                byte[] func_no = Encoding.Default.GetBytes(FuncNo.CONNECT_STATUS);
                #endregion
                #region 序列号
                byte[] serial_no = Encoding.Default.GetBytes(Basis.Serial_No);
                #endregion
                #region 包长度
                int Length = func_no.Length + serial_no.Length + 4;
                byte[] PacketLength = Encoding.Default.GetBytes(Length.ToString("0000"));
                #endregion
                byte[] resArr = new byte[Length];
                Length = 0;

                PacketLength.CopyTo(resArr, 0);
                Length += PacketLength.Length;
                func_no.CopyTo(resArr, PacketLength.Length);
                Length += func_no.Length;
                serial_no.CopyTo(resArr, Length);
                Length += serial_no.Length;
                string str = Encoding.ASCII.GetString(resArr);
                Send(socket, resArr);
                return true;
            });
        }

        /// <summary>
        /// 公话认证
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> Authentication(Socket socket)
        {
            return await Task.Run(() =>
            {
                #region 头
                //功能号
                byte[] func_no = Encoding.Default.GetBytes(FuncNo.PHONE_AUTHEN);
                //序列号
                byte[] serial_no = Encoding.Default.GetBytes(Basis.Serial_No);
                #endregion

                #region 包内容
                //公话ID(device_id)
                byte[] device_id = System.Text.Encoding.Default.GetBytes(Basis.Device_Id);
                byte[] reserved = new byte[8] {32,32,32,32, 32, 32, 32, 32 };
                #endregion

                #region 头
                //包长度
                int Length = func_no.Length + serial_no.Length + device_id.Length + reserved.Length + 4;
                byte[] PacketLength = Encoding.Default.GetBytes(Length.ToString("0000"));
                #endregion

                #region 组装
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
                reserved.CopyTo(resArr, Length);
                Length += reserved.Length;
                #endregion
                string str = Encoding.ASCII.GetString(resArr);
                Send(socket,resArr);
                return true;
            });
        }

        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="host"></param>
        /// <param name="port"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static void Send(Socket socket, byte[] data)
        {
            if (!socket.Connected) new AppException("没有连接上");
            int i = socket.Send(data);
            var result = Receive(socket, 1000 * 2); //1*2 seconds timeout.
        }

        /// <summary>
        /// 销毁Socket对象
        /// </summary>
        /// <param name="socket"></param>
        private static void DestroySocket(Socket socket)
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
        public static byte[] Receive(Socket socket, int timeout)
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
            catch (Exception ex)
            {
                return null;
            }
        }
    }

    public class AttendanceTcp
    {
        /// <summary>
        /// 心跳检测
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> HeartbeatDetection(TcpClient tcpClient)
        {
            return await Task.Run(() =>
            {
                #region 功能号
                byte[] func_no = Encoding.Default.GetBytes(FuncNo.CONNECT_STATUS);
                #endregion
                #region 序列号
                byte[] serial_no = Encoding.Default.GetBytes(Basis.Serial_No);
                #endregion
                #region 包长度
                int Length = func_no.Length + serial_no.Length + 4;
                byte[] PacketLength = Encoding.Default.GetBytes(Length.ToString("0000"));
                #endregion
                byte[] resArr = new byte[Length];
                Length = 0;

                PacketLength.CopyTo(resArr, 0);
                Length += PacketLength.Length;
                func_no.CopyTo(resArr, PacketLength.Length);
                Length += func_no.Length;
                serial_no.CopyTo(resArr, Length);
                Length += serial_no.Length;

                Send(tcpClient, resArr);
                return true;
            });
        }

        public static void Send(TcpClient tcpClient, byte[] data)
        {
            //NetworkStream ns = tcpClient.GetStream();
            //ns.WriteByte(data);

            //fs.Close();
            //ns.Close();
            //tcpClient.Close();
        }
    }
}
