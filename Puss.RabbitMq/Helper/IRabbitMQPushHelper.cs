using static Puss.RabbitMq.RabbitMQPushHelper;

namespace Puss.RabbitMq
{
    public interface IRabbitMQPushHelper
    {
        /// <summary>
        /// MQ发送消息
        /// </summary>
        /// <param name="sQueueName">队列名称</param>
        /// <param name="sContent">内容</param>
        void PushMessage(string sQueueName, string sContent);

        /// <summary>
        /// 收到消息并执行委托
        /// </summary>
        /// <param name="sQueueName">消息队列</param>
        /// <param name="MessageHandler">委托</param>
        void PullMessage(string sQueueName, MessageHandler MessageHandler);
    }
}
