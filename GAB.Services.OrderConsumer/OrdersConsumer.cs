namespace GAB.Services.OrderConsumer
{
    using System.Collections.Generic;
    using System.Diagnostics;

    using GAB.Domain;
    using GAB.Infrastructure.Azure;

    public class OrdersConsumer : IOrdersConsumer
    {
        //private readonly InMemoryOrderStorage orderStorage = new InMemoryOrderStorage();
        
        private readonly AzureTableStorageOrderStorage orderStorage = new AzureTableStorageOrderStorage();
        
        public void Consume(List<Order> orders)
        {
            foreach (Order order in orders)
            {
                Stopwatch stopwatch = new Stopwatch();

                stopwatch.Start();

                orderStorage.Store(order);

                stopwatch.Stop();

                Trace.TraceInformation(
                    "Elapsed time during storing order no. {0} was {1} ms.",
                    order.OrderNo,
                    stopwatch.Elapsed.Milliseconds);
            }
        }
    }
}
