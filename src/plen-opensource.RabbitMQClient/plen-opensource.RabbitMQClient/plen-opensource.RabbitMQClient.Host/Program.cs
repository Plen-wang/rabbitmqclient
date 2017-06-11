using System;

namespace SCM.RabbitMQClient.Host
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Listening();

            //SendEventMessage();

            Console.ReadLine();
        }

        private static void Listening()
        {
            RabbitMqClient.Instance.ActionEventMessage += mqClient_ActionEventMessage;
            RabbitMqClient.Instance.OnListening();
        }

        private static void mqClient_ActionEventMessage(EventMessageResult result)
        {
            if (result.EventMessageBytes.EventMessageMarkcode == MessageTypeConst.ZgUpdatePurchaseStatus)
            {
                var message =
                    MessageSerializerFactory.CreateMessageSerializerInstance()
                        .Deserialize<UpdatePurchaseOrderStatusByBillIdMqContract>(result.MessageBytes);

                result.IsOperationOk = true; //处理成功

                Console.WriteLine(message.ModifiedBy);
            }
        }

        private static void SendEventMessage()
        {
            for (var i = 1; i < 10000; i++)
            {
                var originObject = new UpdatePurchaseOrderStatusByBillIdMqContract()
                {
                    UpdatePurchaseOrderStatusType = 1,
                    RelationBillType = 10,
                    RelationBillId = 10016779,
                    UpdateStatus = 30,
                    ModifiedBy = i
                };

                var sendMessage =
                    EventMessageFactory.CreateEventMessageInstance(originObject, MessageTypeConst.ZgUpdatePurchaseStatus);

                RabbitMqClient.Instance.TriggerEventMessage(sendMessage, "CMQ.Purchase", "CMQ.Purchase");

                Console.WriteLine(i);
            }
        }
    }
}