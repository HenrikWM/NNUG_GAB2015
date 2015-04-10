namespace GAB.Services.OrderConsumer
{
    using System.Collections.Generic;

    using GAB.Domain;
    using GAB.Infrastructure.Azure;

    public class OrderConsumer
    {
        public void Consume(List<Order> orders)
        {
            OrderStorage orderStorage = new OrderStorage();

            foreach (Order order in orders)
            {
                orderStorage.Store(order);
            }
        }
    }
}
