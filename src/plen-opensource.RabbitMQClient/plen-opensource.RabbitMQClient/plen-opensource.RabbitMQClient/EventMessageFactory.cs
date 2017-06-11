using System;

namespace SCM.RabbitMQClient
{
    /// <summary>
    /// 创建EventMessage实例。
    /// </summary>
    public class EventMessageFactory
    {
        /// <summary>
        /// 创建EventMessage传输对象。
        /// </summary>
        /// <param name="originObject">原始强类型对象实例。</param>
        /// <param name="eventMessageMarkcode">消息的标记码。</param>
        /// <typeparam name="T">原始对象类型。</typeparam>
        /// <returns>EventMessage.</returns>
        public static EventMessage CreateEventMessageInstance<T>(T originObject, string eventMessageMarkcode)
            where T : class, new()
        {
            var result = new EventMessage()
            {
                CreateDateTime = DateTime.Now,
                EventMessageMarkcode = eventMessageMarkcode
            };

            var bytes = MessageSerializerFactory.CreateMessageSerializerInstance().SerializerBytes<T>(originObject);

            result.EventMessageBytes = bytes;

            return result;
        }
    }
}