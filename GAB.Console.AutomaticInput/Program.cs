namespace GAB.Console.AutomaticInput
{
    using System;
    using System.Collections.Generic;

    using GAB.Domain;
    using GAB.OrderProducer;
    using GAB.Services.OrderConsumer;

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

                RandomOrderListCreator randomOrderListCreator = new RandomOrderListCreator();

                OrderConsumer orderConsumer = new OrderConsumer();

                List<Order> orders = (List<Order>)randomOrderListCreator.Create();

                orderConsumer.Consume(orders);

                Console.WriteLine("{0}Created orders: {1}", NewLine, orderJsonSerializer.Serialize(orders));

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
