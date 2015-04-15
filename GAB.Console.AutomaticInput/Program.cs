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
            const int OrdersToProduce = 100;

            const int SecondsToSleep = 1;

            TimeSpan sleepTime = TimeSpan.FromSeconds(SecondsToSleep);

            try
            {
                OrderJsonSerializer orderJsonSerializer = new OrderJsonSerializer();

                RandomOrdersProducer randomOrdersProducer = new RandomOrdersProducer();

                OrderSender orderSender = new OrderSender();
                
                while (true)
                {
                    List<Order> orders = (List<Order>)randomOrdersProducer.Produce(OrdersToProduce);

                    Console.WriteLine(
                        "{0}Created orders: {1}",
                        FormattingConstants.NewLine,
                        orderJsonSerializer.Serialize(orders));

                    orderSender.SendOrders(orders);

                    Console.WriteLine(
                        "{0}Sent {1} orders to the service bus topic.{0}Sleeping for {2} seconds...",
                        FormattingConstants.NewLine,
                        orders.Count,
                        SecondsToSleep);

                    Thread.Sleep(sleepTime);
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(
                    string.Format(
                        "{0}An error occurred during producing orders: {1}",
                        FormattingConstants.NewLine,
                        e.Message));
            }
        }
    }
}
