namespace GAB.Infrastructure.Azure.Inbound
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
