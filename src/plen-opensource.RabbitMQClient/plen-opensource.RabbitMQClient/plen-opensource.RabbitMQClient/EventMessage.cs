using System;

namespace SCM.RabbitMQClient
{
    /// <summary>
    /// 表示一个事件消息。
    /// </summary>
    [Serializable]
    public sealed class EventMessage
    {
        /// <summary>
        /// 消息的标记码。
        /// </summary>
        public string EventMessageMarkcode { get; set; }

        /// <summary>
        /// 消息的序列化字节流。
        /// </summary>
        public byte[] EventMessageBytes { get; set; }

        /// <summary>
        /// 创建消息的时间。
        /// </summary>
        public DateTime CreateDateTime { get; set; }

        /// <summary>
        /// 生成EventMessageResult对象。
        /// </summary>
        /// <param name="bytes">流</param>
        /// <returns>EventMessageResult instance.</returns>
        internal static EventMessageResult BuildEventMessageResult(byte[] bytes)
        {
            var eventMessage =
                MessageSerializerFactory.CreateMessageSerializerInstance().Deserialize<EventMessage>(bytes);

            var result = new EventMessageResult
            {
                MessageBytes = eventMessage.EventMessageBytes,
                EventMessageBytes = eventMessage
            };

            return result;
        }
    }
}