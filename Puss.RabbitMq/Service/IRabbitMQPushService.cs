using System.Threading.Tasks;
using static Puss.RabbitMQ.RabbitMQPushService;

namespace Puss.RabbitMQ
{
    public interface IRabbitMQPushService
    {
        /// <summary>
        /// MQ发送消息
        /// </summary>
        /// <param name="sQueueName">队列名称</param>
        /// <param name="sContent">内容</param>
        Task PushMessage(string sQueueName, string sContent);

        /// <summary>
        /// 收到消息并执行委托
        /// </summary>
        /// <param name="sQueueName">消息队列</param>
        /// <param name="MessageHandler">委托</param>
        void PullMessage(string sQueueName, MessageHandler MessageHandler);
    }
}
