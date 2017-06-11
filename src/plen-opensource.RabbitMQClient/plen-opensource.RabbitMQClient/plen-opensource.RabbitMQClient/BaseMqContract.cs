using System;

namespace SCM.RabbitMQClient
{
    /// <summary>
    /// 消息契约基类。
    /// </summary>
    [Serializable]
    public abstract class BaseMqContract
    {
        /// <summary>
        /// 消息ID。
        /// </summary>
        public string MessageId { get; set; }

        /// <summary>
        /// 默认构造函数。
        /// </summary>
        protected BaseMqContract()
        {
            MessageId = Guid.NewGuid().ToString(); //创建一个唯一消息ID。
        }
    }
}