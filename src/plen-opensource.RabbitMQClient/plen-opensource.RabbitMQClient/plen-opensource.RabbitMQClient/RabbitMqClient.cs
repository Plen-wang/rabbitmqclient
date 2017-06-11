using System;
using RabbitMQ.Client.Events;
using SCM.RabbitMQClient.Common;
using System.Threading.Tasks;

namespace SCM.RabbitMQClient
{
    /// <summary>
    /// 表示消息到达客户端发起的事件。
    /// </summary>
    /// <param name="result">EventMessageResult 事件消息对象</param>
    public delegate void ActionEvent(EventMessageResult result);

    /// <summary>
    /// 表示RabbitMQ客户端组件。
    /// </summary>
    public class RabbitMqClient : IRabbitMqClient
    {
        #region Static fields

        /// <summary>
        /// 客户端实例私有字段。
        /// </summary>
        private static IRabbitMqClient _instanceClient;

        /// <summary>
        /// 返回全局唯一的RabbitMqClient实例，此。
        /// </summary>
        public static IRabbitMqClient Instance
        {
            get
            {
                if (_instanceClient.IsNull())
                    RabbitMqClientFactory.CreateRabbitMqClientInstance();

                return _instanceClient;
            }

            internal set { _instanceClient = value; }
        }

        #endregion

        #region Instance fields

        /// <summary>
        /// RabbitMqClient 数据上下文。
        /// </summary>
        public RabbitMqClientContext Context { get; set; }

        /// <summary>
        /// 事件激活委托实例。
        /// </summary>
        private ActionEvent _actionMessage;

        /// <summary>
        /// 当侦听的队列中有消息到达时触发的执行事件。
        /// </summary>
        public event ActionEvent ActionEventMessage
        {
            add
            {
                if (_actionMessage.IsNull())
                    _actionMessage += value;
            }
            remove
            {
                if (_actionMessage.IsNotNull())
                    _actionMessage -= value;
            }
        }

        #endregion

        #region send method

        /// <summary>
        /// 触发一个事件且将事件打包成消息发送到远程队列中。
        /// </summary>
        /// <param name="eventMessage">发送的消息实例。</param>
        /// <param name="exChange">RabbitMq的Exchange名称。</param>
        /// <param name="queue">队列名称。</param>
        public void TriggerEventMessage(EventMessage eventMessage, string exChange, string queue)
        {
            Context.SendConnection = RabbitMqClientFactory.CreateConnection(); //获取连接

            using (Context.SendConnection)
            {
                Context.SendChannel = RabbitMqClientFactory.CreateModel(Context.SendConnection); //获取通道

                const byte deliveryMode = 2;
                using (Context.SendChannel)
                {
                    var messageSerializer = MessageSerializerFactory.CreateMessageSerializerInstance(); //反序列化消息

                    var properties = Context.SendChannel.CreateBasicProperties();
                    properties.DeliveryMode = deliveryMode; //表示持久化消息

                    //推送消息
                    Context.SendChannel.BasicPublish(
                        exChange, queue, properties, messageSerializer.SerializerBytes(eventMessage));
                }
            }
        }

        #endregion

        #region receive method

        /// <summary>
        /// 开始侦听默认的队列。
        /// </summary>
        public void OnListening()
        {
            Task.Factory.StartNew(ListenInit);
        }

        /// <summary>
        /// 侦听初始化。
        /// </summary>
        private void ListenInit()
        {
            Context.ListenConnection = RabbitMqClientFactory.CreateConnection(); //获取连接

            Context.ListenConnection.ConnectionShutdown += (o, e) =>
            {
                if (LogLocation.Log.IsNotNull())
                    LogLocation.Log.WriteInfo("SCM.RabbitMQClient", "connection shutdown:" + e.ReplyText);
            };

            Context.ListenChannel = RabbitMqClientFactory.CreateModel(Context.ListenConnection); //获取通道

            var consumer = new EventingBasicConsumer(Context.ListenChannel); //创建事件驱动的消费者类型
            consumer.Received += consumer_Received;

            Context.ListenChannel.BasicQos(0, 1, false); //一次只获取一个消息进行消费
            Context.ListenChannel.BasicConsume(Context.ListenQueueName, false, consumer);
        }

        /// <summary>
        /// 接受到消息。
        /// </summary>
        private void consumer_Received(object sender, BasicDeliverEventArgs e)
        {
            try
            {
                var result = EventMessage.BuildEventMessageResult(e.Body); //获取消息返回对象

                if (_actionMessage.IsNotNull())
                    _actionMessage(result); //触发外部侦听事件

                if (result.IsOperationOk.IsFalse())
                {
                    //未能消费此消息，重新放入队列头
                    Context.ListenChannel.BasicReject(e.DeliveryTag, true);
                }
                else if (Context.ListenChannel.IsClosed.IsFalse())
                {
                    Context.ListenChannel.BasicAck(e.DeliveryTag, false);
                }
            }
            catch (Exception exception)
            {
                if (LogLocation.Log.IsNotNull())
                    LogLocation.Log.WriteException("SCM.RabbitMQClient", exception);
            }
        }

        #endregion

        #region IDispose

        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose()
        {
            if (Context.SendConnection.IsNull()) return;

            if (Context.SendConnection.IsOpen)
                Context.SendConnection.Close();

            Context.SendConnection.Dispose();
        }

        #endregion
    }
}