namespace GAB.Console.ConsumerOutput
{
    using System;
    using System.Threading;

    using GAB.Core;
    using GAB.Services.OrderConsumer;

    class Program
    {
        private static void Main(string[] args)
        {
            OrderStorage orderStorage = new OrderStorage();
            
            const double OrdersToProducePerSecond = 10;

            const int SecondsToSleep = 1;

            TimeSpan sleepTime = TimeSpan.FromSeconds(SecondsToSleep);

            int lastTotalNumberOfOrders = 0;

            double throughPut = 0;
            
            while (true)
            {
                int currentTotalNumberOfOrders = orderStorage.GetTotalNumberOfOrders();

                int ordersDelta = currentTotalNumberOfOrders - lastTotalNumberOfOrders;

                if (ordersDelta > 0)
                    throughPut = Math.Round(ordersDelta / OrdersToProducePerSecond, 2);

                Console.WriteLine(
                        "{0}Total number of orders: {1}. Throughput: {2} orders/sec. Sleeping for {3} seconds...",
                        TraceLinePrefixer.GetConsoleLinePrefix(),
                        lastTotalNumberOfOrders,
                        throughPut,
                        SecondsToSleep);

                Thread.Sleep(sleepTime);

                lastTotalNumberOfOrders = orderStorage.GetTotalNumberOfOrders();
            }
        }
    }
}
