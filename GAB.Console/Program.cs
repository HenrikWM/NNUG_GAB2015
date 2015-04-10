namespace GAB.Console
{
    using System;
    using System.Linq;

    using GAB.Domain;
    using GAB.OrderProducer;

    class Program
    {
        private static string orderNo;

        private static string customerNo;

        private static string customerName;

        private static string orderItemNo;

        private static string orderItemName;

        private const string NewLine = "\r\n";

        /// <summary>
        /// Sample usage from console:
        /// 
        /// ..\bin\Debug>GAB.Console.exe 1 999 "John Doe" 12345 "Dog food"
        /// 
        /// Created order: {"Customer":{"No":999,"Name":"John Doe"},"OrderItem":{"No":12345,"Name":"Dog food"},"Created":"2015-04-10T14:45:56.7176004+02:00","OrderNo":1}
        /// 
        /// Press any key to quit...
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            try
            {
                // Arguments: Order No. (1), Customer No. (2), Customer Name (3), Order Item No. (4), Order Item Name (5)
                ParseArguments(args);

                OrderCreator orderCreator = new OrderCreator();

                OrderJsonSerializer orderJsonSerializer = new OrderJsonSerializer();

                Order order = orderCreator.Create(orderNo, customerNo, customerName, orderItemNo, orderItemName);

                Console.WriteLine("{0}Created order: {1}", NewLine, orderJsonSerializer.Serialize(order));

                Console.WriteLine("{0}Press any key to quit...", NewLine);

                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("{0}An error occurred: {1}", NewLine, e.Message);
            }
        }

        private static void ParseArguments(string[] args)
        {
            if (args == null || args.Length > 5)
                throw new InvalidOperationException("Wrong number of parameters. Should be 5.");

            if (args.Any())
            {
                orderNo = ParseArgument(args[0]);
                customerNo = ParseArgument(args[1]);
                customerName = ParseArgument(args[2]);
                orderItemNo = ParseArgument(args[3]);
                orderItemName = ParseArgument(args[4]);
            }
        }

        private static string ParseArgument(string arg)
        {
            if (string.IsNullOrEmpty(arg))
                throw new InvalidOperationException("An argument was empty.");

            return arg;
        }
    }
}
