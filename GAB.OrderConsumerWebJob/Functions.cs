using System.IO;

using Microsoft.Azure.WebJobs;

namespace GAB.OrderConsumerWebJob
{
    using GAB.Core;
    using GAB.Infrastructure.Azure;
    using GAB.Services.OrderConsumer;

    public class Functions
    {
        public static void ProcessTopicMessage(
            [ServiceBusTrigger(ServiceBusConstants.OrderDispatchTopicName, ServiceBusConstants.SubscriptionName)] string message,
            TextWriter log)
        {
            //OrderJsonDeserializer OrderJsonDeserializer = new OrderJsonDeserializer();
            
            //OrdersConsumer OrderConsumer = new OrdersConsumer();

            log.WriteLine("{0}Process order {1}", FormattingConstants.NewLine, message);

            //Order order = OrderJsonDeserializer.Deserialize(message);

            //TimedOperation.Run(() => OrderConsumer.Consume(order));

            //log.WriteLine("{0}Processed order no. {1}", FormattingConstants.NewLine, order.OrderNo);
        }
    }
}
