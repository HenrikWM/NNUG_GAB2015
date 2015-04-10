namespace GAB.Console.AutomaticInput
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    using GAB.Domain;
    using GAB.Services.OrderConsumer;
    using GAB.Services.OrderProducer;

    class Program
    {
        private const string NewLine = "\r\n";

        private static void Main(string[] args)
        {
            // HandleManualInput(args)

            HandleAutogenerationInput();
        }

        /// <summary>
        /// Sample usage from console:
        /// 
        /// ..\bin\Debug>GAB.Console.exe
        /// 
        /// Press any key to quit...
        /// </summary>
        private static void HandleAutogenerationInput()
        {
            try
            {
                OrderJsonSerializer orderJsonSerializer = new OrderJsonSerializer();
              
                RandomOrderProducer randomOrderProducer = new RandomOrderProducer();

                OrderConsumer orderConsumer = new OrderConsumer();

                const int NumberOfOrders = 100;

                List<Order> orders = (List<Order>)randomOrderProducer.ProduceRandomOrders(NumberOfOrders);
                
                Stopwatch stopwatch = new Stopwatch();

                stopwatch.Start();

                orderConsumer.Consume(orders);

                stopwatch.Stop();

                Console.WriteLine("{0}Created orders: {1}", NewLine, orderJsonSerializer.Serialize(orders));

                double throughputInMinutes = NumberOfOrders / stopwatch.Elapsed.TotalMinutes;

                Console.WriteLine("{0}Throughput: {1} orders/min", NewLine, Math.Round(throughputInMinutes, 0));

                Console.WriteLine("{0}Press any key to quit...", NewLine);

                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}An error occurred: {1}", NewLine, e.Message);
            }
        }
    }
}
