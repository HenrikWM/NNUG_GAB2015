namespace GAB.Services.OrderConsumer
{
    using System.Collections.Generic;
    using System.Diagnostics;

    using GAB.Domain;
    using GAB.Infrastructure.Azure;

    public class OrdersConsumer : IOrdersConsumer
    {
        public void Consume(List<Order> orders)
        {
            InMemoryOrderStorage orderStorage = new InMemoryOrderStorage();

            Stopwatch stopwatch = new Stopwatch();

            foreach (Order order in orders)
            {
                stopwatch.Start();

                orderStorage.Store(order);

                stopwatch.Stop();

                Trace.TraceInformation("Elapsed time in storing order no. {0} was {1}.", order.OrderNo, stopwatch.Elapsed);
            }
        }
    }
}
