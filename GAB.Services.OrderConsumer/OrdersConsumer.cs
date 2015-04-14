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
            //InMemoryOrderStorage orderStorage = new InMemoryOrderStorage();
            AzureTableStorageOrderStorage orderStorage = new AzureTableStorageOrderStorage();
            
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
