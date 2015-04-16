namespace GAB.Infrastructure.Azure.Producer
{
    using System.Collections.Generic;
    using System.Diagnostics;

    using GAB.Core;

    using Microsoft.ServiceBus;
    using Microsoft.ServiceBus.Messaging;

    public class AzureServiceBusOrderSender
    {
        private readonly MessageSender sender;
        
        public AzureServiceBusOrderSender()
        {
            string keyName = Config.GetConfigurationSetting("ServiceBusKeyName");

            string accessKey = Config.GetConfigurationSetting("ServiceBusAccessKey");
            
            string baseAddress = Config.GetConfigurationSetting("ServiceBusAddress");
            
            TokenProvider tokenProvider = TokenProvider.CreateSharedAccessSignatureTokenProvider(keyName, accessKey);

            MessagingFactory messagingFactory = MessagingFactory.Create(baseAddress, tokenProvider);

            sender = messagingFactory.CreateMessageSender(ServiceBusConstants.OrderDispatchTopicName);
        }

        public void SendOrders(List<Order> orders)
        {
            IEnumerable<BrokeredMessage> batchOfOrders = CreateBatchOfMessages(orders);

            Trace.TraceInformation(
                "{0}Sending batch of {1} orders to topic '{2}'",
                TraceLinePrefixer.GetConsoleLinePrefix(),
                orders.Count,
                ServiceBusConstants.OrderDispatchTopicName);

            sender.SendBatch(batchOfOrders);
        }

        public void SendOrder(Order order)
        {
            Trace.TraceInformation(
                "{0}Sending one order to topic '{1}'",
                TraceLinePrefixer.GetConsoleLinePrefix(),
                ServiceBusConstants.OrderDispatchTopicName);

            BrokeredMessage brokeredMessage = CreateBrokeredMessage(order);

            sender.Send(brokeredMessage);
        }

        private IEnumerable<BrokeredMessage> CreateBatchOfMessages(IEnumerable<Order> orders)
        {
            List<BrokeredMessage> batch = new List<BrokeredMessage>();

            foreach (Order order in orders)
            {
                BrokeredMessage brokeredMessage = CreateBrokeredMessage(order);

                batch.Add(brokeredMessage);
            }

            return batch;
        }

        private BrokeredMessage CreateBrokeredMessage(Order order)
        {
            return new BrokeredMessage(order);
        }
    }
}