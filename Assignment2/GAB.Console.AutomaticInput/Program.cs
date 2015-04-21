namespace GAB.Console.AutomaticInput
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;

    using GAB.Core;
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

                IOrderSender orderSender = // TODO: Create new class AzureServiceBusTopicOrderSender 
                
                while (true)
                {
                    List<Order> orders = (List<Order>)randomOrdersProducer.Produce(OrdersToProducePerSecond);

                    Console.WriteLine(
                        "{0}Created {1} orders",
                        TraceLinePrefixer.GetConsoleLinePrefix(),
                        orders.Count);

                    orderSender.SendOrders(orders);

                    Console.WriteLine(
                        "{0}Sent the orders to the service bus topic. Sleeping for {1} seconds...",
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
