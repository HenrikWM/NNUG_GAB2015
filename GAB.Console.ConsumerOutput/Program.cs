namespace GAB.Console.ConsumerOutput
{
    using System;
    using System.Threading;

    using GAB.Core;
    using GAB.Infrastructure.Azure.Consumer;
    using GAB.Services.OrderConsumer;

    class Program
    {
        private static void Main(string[] args)
        {
            IOrderStorage orderStorage = new AzureTableStorageOrderStorage();

            const double OrdersToProducePerSecond = 10;

            const int SecondsToSleep = 1;

            TimeSpan sleepTime = TimeSpan.FromSeconds(SecondsToSleep);

            int lastTotalNumberOfOrders = 0;

            while (true)
            {
                int currentTotalNumberOfOrders = orderStorage.GetTotalNumberOfOrders();

                Console.WriteLine(
                        "{0}Total number of orders: {1}",
                        TraceLinePrefixer.GetConsoleLinePrefix(),
                        currentTotalNumberOfOrders);

                int ordersDelta = currentTotalNumberOfOrders - lastTotalNumberOfOrders;

                if (ordersDelta > 0 && lastTotalNumberOfOrders > 0)
                {
                    double throughPut = Math.Round(ordersDelta / OrdersToProducePerSecond, 2);

                    Console.WriteLine(
                        "{0}Throughput: {1} orders/sec. Sleeping for {2} seconds...",
                        TraceLinePrefixer.GetConsoleLinePrefix(),
                        throughPut,
                        SecondsToSleep);

                    Thread.Sleep(sleepTime);
                }

                lastTotalNumberOfOrders = orderStorage.GetTotalNumberOfOrders();
            }
        }
    }
}
