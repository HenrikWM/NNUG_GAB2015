namespace GAB.Infrastructure.Azure.Consumer
{
    using System.Collections.Generic;

    using GAB.Core;

    public class InMemoryOrderStorage
    {
        private readonly List<Order> orders = new List<Order>(); 

        public void Store(Order order)
        {
            orders.Add(order);
        }
    }
}
