using System.IO;
using System.Xml.Serialization;

namespace SCM.RabbitMQClient
{
    /// <summary>
    /// 面向XML的消息序列化。
    /// </summary>
    public class MessageXmlSerializer : IMessageSerializer
    {
        /// <summary>
        /// 序列化成bytes。
        /// </summary>
        /// <typeparam name="T">消息的类型。</typeparam>
        /// <param name="message">消息的实例。</param>
        /// <returns></returns>
        public byte[] SerializerBytes<T>(T message) where T : class, new()
        {
            var xmlSerializer = new XmlSerializer(typeof (T));
            using (var msStream = new MemoryStream())
            {
                xmlSerializer.Serialize(msStream, message);
                return msStream.ToArray();
            }
        }

        /// <summary>
        /// 序列化消息为XML字符串。
        /// </summary>
        /// <param name="message">消息类型</param>
        /// <typeparam name="T">消息实例</typeparam>
        /// <returns></returns>
        public string SerializerXmlString<T>(T message) where T : class, new()
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var msStream = new StringWriter())
            {
                xmlSerializer.Serialize(msStream, message);
                return msStream.ToString();
            }
        }

        /// <summary>
        /// 反序列化消息。
        /// </summary>
        /// <typeparam name="T">消息的类型。</typeparam>
        /// <param name="bytes">bytes。</param>
        /// <returns></returns>
        public T Deserialize<T>(byte[] bytes) where T : class, new()
        {
            var xmlSerializer = new XmlSerializer(typeof (T));
            using (var msStream = new MemoryStream(bytes))
            {
                return xmlSerializer.Deserialize(msStream) as T;
            }
        }
    }
}