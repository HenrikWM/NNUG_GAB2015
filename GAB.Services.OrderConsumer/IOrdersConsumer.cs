namespace GAB.Services.OrderConsumer
{
    using System.Collections.Generic;

    using GAB.Core;

    public interface IOrdersConsumer
    {
        void Consume(Order order);

        void Consume(List<Order> orders);
    }
}