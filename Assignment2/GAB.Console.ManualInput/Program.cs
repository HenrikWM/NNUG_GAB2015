namespace GAB.Console.ManualInput
{
    using System;

    using GAB.Core;
    using GAB.Infrastructure.Azure.Producer;
    using GAB.Services.OrderProducer;

    class Program
    {
        private static string orderNo;

        private static string customerNo;

        private static string customerName;

        private static string orderItemNo;

        private static string orderItemName;
        
        static void Main(string[] args)
        {
            HandleManualInput(args);
        }

        private static void HandleManualInput(string[] args)
        {
            try
            {
                ParseArguments(args);

                OrderProducer orderProducer = new OrderProducer();
                
                IOrderSender orderSender = // TODO: Create new class AzureServiceBusTopicOrderSender 

                Order order = orderProducer.Produce(orderNo, customerNo, customerName, orderItemNo, orderItemName);

                Console.WriteLine("{0}Created order: {0}{1}", TraceLinePrefixer.GetConsoleLinePrefix(), order);

                orderSender.SendOrder(order);
                
                Console.WriteLine(
                    "{0}Sent order to the service bus topic.",
                    TraceLinePrefixer.GetConsoleLinePrefix());

                Console.WriteLine("{0}Press any key to quit...", TraceLinePrefixer.GetConsoleLinePrefix());

                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}An error occurred: {1}", TraceLinePrefixer.GetConsoleLinePrefix(), e.Message);
            }
        }

        private static void ParseArguments(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                GetManualOrderEntry();
                return;
            }

            if (args.Length != 5)
                throw new InvalidOperationException("Invalid number of parameters. Should be 5.");

            orderNo = ParseArgument(args[0]);
            customerNo = ParseArgument(args[1]);
            customerName = ParseArgument(args[2]);
            orderItemNo = ParseArgument(args[3]);
            orderItemName = ParseArgument(args[4]);
        }

        private static void GetManualOrderEntry()
        {
            orderNo = GetNewInfo("orderNo");
            customerNo = GetNewInfo("customerNo");
            customerName = GetNewInfo("customerName");
            orderItemNo = GetNewInfo("orderItemNo");
            orderItemName = GetNewInfo("orderItemName");
        }

        private static string GetNewInfo(string key)
        {
            Console.WriteLine("Please enter : {0}", key);
            return Console.ReadLine();
        }

        private static string ParseArgument(string arg)
        {
            if (string.IsNullOrEmpty(arg))
                throw new InvalidOperationException("Argument was empty.");

            return arg;
        }
    }
}
