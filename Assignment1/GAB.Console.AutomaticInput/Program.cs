namespace GAB.Console.AutomaticInput
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;

    using GAB.Core;
    using GAB.Infrastructure.Azure.Consumer;
    using GAB.Services.OrderConsumer;
    using GAB.Services.OrderProducer;

    class Program
    {
        private static void Main(string[] args)
        {
            HandleAutomaticInput();
        }

        private static void HandleAutomaticInput()
        {
            const int OrdersToProducePerSecond = 10;

            const int SecondsToSleep = 1;

            TimeSpan sleepTime = TimeSpan.FromSeconds(SecondsToSleep);

            try
            {
                RandomOrdersProducer randomOrdersProducer = new RandomOrdersProducer();

                IOrderStorage orderStorage = new InMemoryOrderStorage(); // TODO: replace with new class AzureTableStorageOrderStorage
                
                while (true)
                {
                    List<Order> orders = (List<Order>)randomOrdersProducer.Produce(OrdersToProducePerSecond);

                    Console.WriteLine(
                        "{0}Created {1} orders",
                        TraceLinePrefixer.GetConsoleLinePrefix(),
                        orders.Count);

                    foreach (Order order in orders)
                    {
                        orderStorage.Store(order);
                    }

                    Console.WriteLine(
                        "{0}Sent the orders to order storage. Sleeping for {1} seconds...",
                        TraceLinePrefixer.GetConsoleLinePrefix(),
                        SecondsToSleep);

                    Thread.Sleep(sleepTime);
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(
                    string.Format(
                        "{0}An error occurred during producing orders: {1}",
                        TraceLinePrefixer.GetConsoleLinePrefix(),
                        e.Message));
            }
        }
    }
}
