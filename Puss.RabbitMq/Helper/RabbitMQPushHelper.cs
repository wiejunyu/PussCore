using Puss.RabbitMQ;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Puss.RabbitMQ
{
    public class RabbitMQPushHelper: IRabbitMQPushHelper
    {
        private static IConnection connection;
        public IConnection GetConection()
        {

            if (connection == null)
            {
                //1.1.实例化连接工厂
                var factory = new ConnectionFactory()
                {
                    HostName = Connection.RabbitMQ_HostName,
                    UserName = Connection.RabbitMQ_UserName,
                    Password = Connection.RabbitMQ_PassWord
                };
                //2. 建立连接
                connection = factory.CreateConnection();
            }
            return connection;
        }

        /// <summary>
        /// MQ发送消息
        /// </summary>
        /// <param name="sQueueName">队列名称</param>
        /// <param name="sContent">内容</param>
        public void PushMessage(string sQueueName, string sContent)
        {
            //3. 创建信道
            using (var channel = GetConection().CreateModel())
            {
                //4. 申明队列(指定durable:true,告知rabbitmq对消息进行持久化)
                channel.QueueDeclare(queue: sQueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
                //5. 构建byte消息数据包
                var body = Encoding.UTF8.GetBytes(sContent);
                //6. 发送数据包
                channel.BasicPublish(exchange: "", routingKey: sQueueName, basicProperties: null, body: body);
            }
        }

        /// <summary>
        /// 收到消息的委托
        /// </summary>
        public delegate bool MessageHandler(string Email);
        /// <summary>
        /// 收到消息并执行委托
        /// </summary>
        /// <param name="sQueueName">消息队列</param>
        /// <param name="MessageHandler">委托</param>
        public void PullMessage(string sQueueName, MessageHandler MessageHandler)
        {
            //3. 创建信道
            using (var channel = GetConection().CreateModel())
            {
                //4. 申明队列(指定durable:true,告知rabbitmq对消息进行持久化)
                channel.QueueDeclare(queue: sQueueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
                //将消息标记为持久性 - 将IBasicProperties.SetPersistent设置为true
                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;
                //5. 构造消费者实例
                var consumer = new EventingBasicConsumer(channel);
                //公平分发,设置prefetchCount : 1来告知RabbitMQ，在未收到消费端的消息确认时，不再分发消息，也就确保了当消费端处于忙碌状态时
                channel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
                //6. 绑定消息接收后的事件委托
                consumer.Received += (model, ea) =>
                {
                    string Message = Encoding.UTF8.GetString(ea.Body);
                    if (MessageHandler(Message))
                    {
                        //8. 启动消费者
                        //autoAck:true；自动进行消息确认，当消费端接收到消息后，就自动发送ack信号，不管消息是否正确处理完毕
                        //autoAck:false；关闭自动消息确认，通过调用BasicAck方法手动进行消息确认
                        channel.BasicConsume(queue: sQueueName, autoAck: true, consumer: consumer);
                        //// 7. 发送消息确认信号（手动消息确认）
                        //channel.BasicAck(deliveryTag: ea.DeliveryTag, multiple: false);
                    }
                };

            }
        }
    }
}
