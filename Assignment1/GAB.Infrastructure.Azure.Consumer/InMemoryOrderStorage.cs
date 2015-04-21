namespace GAB.Infrastructure.Azure.Consumer
{
    using System.Collections.Generic;

    using GAB.Core;
    using GAB.Services.OrderConsumer;

    public class InMemoryOrderStorage : IOrderStorage
    {
        private readonly List<Order> orders = new List<Order>();

        public int GetTotalNumberOfOrders()
        {
            return orders.Count;
        }

        public void Store(Order order)
        {
            orders.Add(order);
        }
    }
}