namespace GAB.Infrastructure.Azure.Consumer
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;

    using GAB.Core;
    using GAB.Services.OrderConsumer;

    using Microsoft.Azure;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Table;

    public class AzureTableStorageOrderStorage : IOrderStorage
    {
        private static readonly CloudStorageAccount StorageAccount;
        private static readonly CloudTable OrderTableClient;

        static AzureTableStorageOrderStorage()
        {
            StorageAccount = Config.GetCloudStorageAccount("StorageConnectionString");
            var tableClient = StorageAccount.CreateCloudTableClient();
            OrderTableClient = tableClient.GetTableReference(TableStorageConstants.OrdersTableName);
            OrderTableClient.CreateIfNotExists();
        }

        public int GetTotalNumberOfOrders()
        {
            TableQuery<OrderEntity> query = new TableQuery<OrderEntity>();

            IEnumerable<OrderEntity> orderEntities = OrderTableClient.ExecuteQuery(query);

            if (orderEntities != null && orderEntities.Any())
                return orderEntities.Count();
            
            return 0;
        }

        private class OrderEntity : TableEntity
        {
            public OrderEntity()
            {
                Customer = new Customer();
                OrderItem = new OrderItem();
                Created = DateTime.MinValue;
                OrderNo = 0;

                PartitionKey = Customer.No.ToString(CultureInfo.InvariantCulture);
                RowKey = OrderNo.ToString(CultureInfo.InvariantCulture);
            }

            public Customer Customer { get; set; }

            public OrderItem OrderItem { get; set; }

            public DateTime Created { get; set; }

            public int OrderNo { get; set; }
        }

        public void Store(Order order)
        {


            OrderEntity orderEntity = new OrderEntity {
                Created = order.Created,
                Customer = order.Customer,
                OrderItem = order.OrderItem
            };

            TableOperation insertOperation = TableOperation.Insert(orderEntity);

            // Execute the insert operation.
            TableResult result = OrderTableClient.Execute(insertOperation);

            if (result.HttpStatusCode >= 400)
            {
                Trace.TraceInformation("Issue storing in storing order no. {0} was {1} : {2}.", order.OrderNo, result.HttpStatusCode, result.Result);
            }
        }
    }
}
