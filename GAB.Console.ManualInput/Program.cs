﻿namespace GAB.Console.ManualInput
{
    using System;
    using System.Linq;

    using GAB.Domain;
    using GAB.Services.OrderProducer;

    class Program
    {
        private static string orderNo;

        private static string customerNo;

        private static string customerName;

        private static string orderItemNo;

        private static string orderItemName;

        private const string NewLine = "\r\n";

        static void Main(string[] args)
        {
            HandleManualInput(args);
        }

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
        private static void HandleManualInput(string[] args)
        {
            try
            {
                // Arguments: Order No. (1), Customer No. (2), Customer Name (3), Order Item No. (4), Order Item Name (5)
                ParseArguments(args);

                OrderJsonSerializer orderJsonSerializer = new OrderJsonSerializer();

                OrderProducer orderProducer = new OrderProducer();

                Order order = orderProducer.Produce(orderNo, customerNo, customerName, orderItemNo, orderItemName);

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
