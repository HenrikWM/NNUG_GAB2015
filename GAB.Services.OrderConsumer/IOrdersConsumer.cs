namespace GAB.Services.OrderConsumer
{
    using System.Collections.Generic;

    using GAB.Domain;

    public interface IOrdersConsumer
    {
        void Consume(List<Order> orders);
    }
}