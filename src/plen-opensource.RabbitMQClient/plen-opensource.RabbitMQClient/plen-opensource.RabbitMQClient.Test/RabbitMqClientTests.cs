using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SCM.RabbitMQClient.Test
{
    /// <summary>
    /// RabbitMqClient 单元测试。
    /// </summary>
    [TestClass]
    public class RabbitMqClientTests
    {
        /// <summary>
        /// 当RabbitMqClient中的Instance属性为NULL时自动调用RabbitMqClientFactory进行创建。
        /// </summary>
        [TestMethod]
        public void RabbitMqClient_Instance_IsNull_AutoCall_RabbitMqClientFactory()
        {
            var testObject = RabbitMqClient.Instance;

            Assert.IsNotNull(testObject);

            Assert.IsNotNull(testObject.Context.InstanceCode);
        }
    }
}
