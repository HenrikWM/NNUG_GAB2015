namespace GAB.Services.OrderProducer
{
    using System.Collections.Generic;

    using GAB.Core;
    using GAB.Infrastructure.Azure.Inbound;

    public class OrderSender 
    {
        readonly AzureServiceBusOrderSender azureServiceBusOrderSender = new AzureServiceBusOrderSender();

        public void SendOrders(List<Order> orders)
        {
            azureServiceBusOrderSender.SendOrders(orders);
        }

        public void SendOrder(Order order)
        {
            azureServiceBusOrderSender.SendOrder(order);
        }
    }
}