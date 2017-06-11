using RabbitMQ.Client;

namespace SCM.RabbitMQClient
{
    /// <summary>
    /// RabbitMqClient上下文。
    /// </summary>
    public class RabbitMqClientContext
    {
        /// <summary>
        /// 用于发送消息的Connection。
        /// </summary>
        public IConnection SendConnection { get; internal set; }

        /// <summary>
        /// 用于发送消息的Channel。
        /// </summary>
        public IModel SendChannel { get; internal set; }

        /// <summary>
        /// 用户侦听的Connection。
        /// </summary>
        public IConnection ListenConnection { get; internal set; }

        /// <summary>
        /// 用户侦听的Channel。
        /// </summary>
        public IModel ListenChannel { get; internal set; }

        /// <summary>
        /// 默认侦听的队列名称。
        /// </summary>
        public string ListenQueueName { get; internal set; }

        /// <summary>
        /// 实例编号。
        /// </summary>
        public string InstanceCode { get; set; }
    }
}