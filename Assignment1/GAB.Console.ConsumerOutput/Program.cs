using System;
using System.Diagnostics;
using System.Threading;
using GAB.Core;
using GAB.Services.OrderConsumer;

namespace GAB.Console.ConsumerOutput
{
    class Program
    {
        private static void Main(string[] args)
        {
            IOrderStorage orderStorage = new AzureTableStorageOrderStorage();

            const double OrdersToProducePerSecond = 10;

            const int SecondsToSleep = 1;

            TimeSpan sleepTime = TimeSpan.FromSeconds(SecondsToSleep);

            int lastTotalNumberOfOrders = 0;
            Stopwatch watch = new Stopwatch();

            while (true)
            {
                int currentTotalNumberOfOrders = orderStorage.GetTotalNumberOfOrders();

                System.Console.WriteLine(
                        "{0}Total number of orders: {1}",
                        TraceLinePrefixer.GetConsoleLinePrefix(),
                        currentTotalNumberOfOrders);

                int ordersDelta = currentTotalNumberOfOrders - lastTotalNumberOfOrders;

                if (ordersDelta > 0 && lastTotalNumberOfOrders > 0)
                {
                    double timeUsed = Math.Max(1, watch.ElapsedMilliseconds)/1000d;
                    double throughPut = Math.Round(ordersDelta / timeUsed , 2);

                    System.Console.WriteLine(
                        "{0}Throughput: {1} orders/sec. Sleeping for {2} seconds...",
                        TraceLinePrefixer.GetConsoleLinePrefix(),
                        throughPut,
                        SecondsToSleep);

                    Thread.Sleep(sleepTime);
                }

                lastTotalNumberOfOrders = orderStorage.GetTotalNumberOfOrders();
                watch.Restart();
            }
        }
    }
}
