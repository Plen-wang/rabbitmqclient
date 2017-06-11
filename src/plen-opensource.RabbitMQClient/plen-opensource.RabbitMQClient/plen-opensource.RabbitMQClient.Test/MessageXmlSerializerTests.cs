using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SCM.RabbitMQClient.Test
{
    [TestClass]
    public class MessageXmlSerializerTests
    {
        [Serializable]
        public class CrmMessage 
        {
            public string CustomId { get; set; }
        }

        [TestMethod]
        public void MessageXmlSerializer_SerializerBytes_UnitTest()
        {
            IMessageSerializer testObject = new MessageXmlSerializer();

            var testData = new CrmMessage()
            {
                CustomId = "1433"
            };

            var testResult = testObject.SerializerBytes(testData);

            Assert.IsTrue(testResult.Length > 0);
        }

        [TestMethod]
        public void MessageXmlSerializer_SerializerString_UnitTest()
        {
            IMessageSerializer testObject = new MessageXmlSerializer();

            var testData = new CrmMessage()
            {
                CustomId = "1433"
            };

            var testResult = testObject.SerializerXmlString(testData);

            Assert.IsTrue(testResult.Length > 0);
        }

        [TestMethod]
        public void MessageXmlSerializer_Deserialize_UnitTest()
        {
            IMessageSerializer testObject = new MessageXmlSerializer();

            var testData = new CrmMessage()
            {
                CustomId = "1433",
            };

            var bytes = testObject.SerializerBytes(testData);

            var testResult = testObject.Deserialize<CrmMessage>(bytes);

            Assert.AreEqual(testResult.CustomId, "1433");
        }
    }
}