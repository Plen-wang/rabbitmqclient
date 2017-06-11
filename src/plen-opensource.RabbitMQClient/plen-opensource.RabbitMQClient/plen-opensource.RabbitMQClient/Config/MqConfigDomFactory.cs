using System;
using System.Configuration;
using SCM.RabbitMQClient.Common;

namespace SCM.RabbitMQClient.Config
{
    /// <summary>
    /// <see cref="SCM.RabbitMQClient.Config.MqConfigDom"/>创建工厂。
    /// </summary>
    internal class MqConfigDomFactory
    {
        /// <summary>
        /// 创建MqConfigDom一个实例。
        /// </summary>
        /// <returns>MqConfigDom</returns>
        internal static MqConfigDom CreateConfigDomInstance()
        {
            return GetConfigFormAppStting();
        }

        /// <summary>
        /// 获取物理配置文件中的配置项目。
        /// </summary>
        /// <returns></returns>
        private static MqConfigDom GetConfigFormAppStting()
        {
            var result = new MqConfigDom();

            var mqHost = ConfigurationManager.AppSettings["MqHost"];
            if (mqHost.IsNullOrEmpty())
                throw new Exception("RabbitMQ地址配置错误");
            result.MqHost = mqHost;

            var mqUserName = ConfigurationManager.AppSettings["MqUserName"];
            if (mqUserName.IsNullOrEmpty())
                throw new Exception("RabbitMQ用户名不能为NULL");

            result.MqUserName = mqUserName;

            var mqPassword = ConfigurationManager.AppSettings["MqPassword"];
            if (mqPassword.IsNullOrEmpty())
                throw new Exception("RabbitMQ密码不能为NULL");

            result.MqPassword = mqPassword;

            var mqListenQueueName = ConfigurationManager.AppSettings["MqListenQueueName"];
            if (mqListenQueueName.IsNullOrEmpty())
                throw new Exception("RabbitMQClient 默认侦听的MQ队列名称不能为NULL");

            result.MqListenQueueName = mqListenQueueName;

            return result;
        }
    }
}