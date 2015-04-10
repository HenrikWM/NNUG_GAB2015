namespace GAB.Services.OrderConsumer
{
    using System.Collections.Generic;
    using System.Diagnostics;

    using GAB.Domain;
    using GAB.Infrastructure.Azure;

    public class OrderConsumer
    {
        public void Consume(List<Order> orders)
        {
            AzureOrderStorage azureOrderStorage = new AzureOrderStorage();

            Stopwatch stopwatch = new Stopwatch();

            foreach (Order order in orders)
            {
                stopwatch.Start();

                azureOrderStorage.Store(order);

                stopwatch.Stop();

                Trace.TraceInformation("Elapsed time in Azure storing order no. {0} was {1}.", order.OrderNo, stopwatch.Elapsed);
            }
        }
    }
}
