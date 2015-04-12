namespace GAB.Console.AutomaticInput
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading;

    using GAB.Domain;
    using GAB.Services.OrderConsumer;
    using GAB.Services.OrderProducer;

    class Program
    {
        private const string NewLine = "\r\n";

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
            try
            {
                OrderJsonSerializer orderJsonSerializer = new OrderJsonSerializer();

                RandomOrdersProducer randomOrdersProducer = new RandomOrdersProducer();

                OrdersConsumer ordersConsumer = new OrdersConsumer();

                const int OrdersPerSecond = 100;
                
                List<Order> orders = (List<Order>)randomOrdersProducer.Produce(OrdersPerSecond);

                Console.WriteLine("{0}Created orders: {1}", NewLine, orderJsonSerializer.Serialize(orders));
                
                double elapedSeconds = TimedOperation.Run(() => ordersConsumer.Consume(orders));

                double throughputPerSecond = OrdersPerSecond / elapedSeconds;

                Console.WriteLine("{0}Throughput: {1} orders/sec", NewLine, Math.Round(throughputPerSecond, 0));
            }
            catch (Exception e)
            {
                Trace.WriteLine(string.Format("{0}An error occurred: {1}", NewLine, e.Message));
            }
        }
    }
}
