namespace GAB.Services.OrderProducer
{
    using System.Collections.Generic;

    using GAB.Core;

    public interface IOrderSender
    {
        void SendOrders(List<Order> orders);

        void SendOrder(Order order);
    }
}