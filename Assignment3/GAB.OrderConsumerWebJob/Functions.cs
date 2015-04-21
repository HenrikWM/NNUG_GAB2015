using System.IO;

using Microsoft.Azure.WebJobs;

namespace GAB.OrderConsumerWebJob
{
    using System;

    using GAB.Core;
    using GAB.Infrastructure.Azure;
    using GAB.Services.OrderConsumer;

    using Microsoft.ServiceBus.Messaging;

    public class Functions
    {
        public static void ProcessTopicMessage(
            // TODO: ServiceBusTrigger,
            // TODO: Table,
            TextWriter log)
        {
            try
            {
                double elapsedTimeInMilliseconds = TimedOperation.Run(() => ProcessOrderMessage(message, tableBinding, log));
                
                log.WriteLine(
                    "Done processing order message {0}. Took {1} ms",
                    message,
                    elapsedTimeInMilliseconds);
            }
            catch (Exception e)
            {
                log.WriteLine("Error occurred during processing of an order message. Error: {0}. Message: {1}", e.Message, message);
            }
        }

        private static void ProcessOrderMessage(BrokeredMessage message, ICollector<Order> tableBinding, TextWriter log)
        {
            log.WriteLine("Processing order message {0}", message);

            Order order = message.GetBody<Order>();

            tableBinding.Add(order);
        }
    }
}
