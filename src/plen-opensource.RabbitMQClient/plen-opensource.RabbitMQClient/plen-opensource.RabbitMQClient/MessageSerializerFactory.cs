namespace SCM.RabbitMQClient
{
    /// <summary>
    /// IMessageSerializer 创建工厂。
    /// </summary>
    public class MessageSerializerFactory
    {
        /// <summary>
        /// 创建一个消息序列化组件。
        /// </summary>
        /// <returns></returns>
        public static IMessageSerializer CreateMessageSerializerInstance()
        {
            return new MessageXmlSerializer();
        }
    }
}