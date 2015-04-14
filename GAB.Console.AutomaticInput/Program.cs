namespace GAB.Console.AutomaticInput
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;

    using GAB.Domain;
    using GAB.Services.OrderProducer;

    class Program
    {
        private static void Main(string[] args)
        {
            HandleAutomaticInput();
        }

        /// <summary>
        /// Sample usage from console:
        /// 
        /// ..\bin\Debug>GAB.Console.exe
        /// 
        /// </summary>
        private static void HandleAutomaticInput()
        {
            const int OrdersToProduce = 100;

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
                        "{0}Sent {1} orders to the service bus topic.",
                        FormattingConstants.NewLine,
                        orders.Count);

                    Thread.Sleep(TimeSpan.FromSeconds(1));

                    // ## START - Flyttes til WebJob
                    //double elapedSeconds = TimedOperation.Run(() => ordersConsumer.Consume(orders));

                    //double throughputPerSecond = OrdersToProduce / elapedSeconds;

                    //double throughputPerSecondRounded = Math.Round(throughputPerSecond, 2);

                    // TODO: replace console.writeline with trace
                    //Console.WriteLine(
                    //    "{0}Throughput: {1} orders/sec",
                    //    FormattingConstants.NewLine,
                    //    throughputPerSecondRounded);
                    // ## STOP - Flyttes til WebJob
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
