using AspectInjector.Broker;
using System;
using Newtonsoft.Json;
using Puss.Data.Models;
using Puss.RabbitMQ;

namespace Puss.Api.Aop
{
    /// <summary>
    /// 苹果请求切面
    /// </summary>
    [Aspect(Scope.Global)]
    [Injection(typeof(AopIphone))]
    public class AopIphone : Attribute
    {
        /// <summary>
        /// 苹果请求之前切面
        /// </summary>
        /// <param name="arguments">参数数组</param>
        [Advice(Kind.Before)]
        public void IphoneEnter(
        [Argument(Source.Arguments)] object[] arguments)
        {
            new RabbitMQPushService().PushMessage(QueueKey.GetGuidBody, JsonConvert.SerializeObject(arguments));
        }
    }
}