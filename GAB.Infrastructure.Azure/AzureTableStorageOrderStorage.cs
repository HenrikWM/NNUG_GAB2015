using System.Collections.Generic;
using System.Diagnostics;

using GAB.Domain;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace GAB.Infrastructure.Azure
{
    using System.Globalization;

    public class AzureTableStorageOrderStorage
    {
        static readonly CloudStorageAccount StorageAccount = Config.GetCloudStorageAccount("StorageConnectionString");
        static readonly CloudTableClient TableClient = StorageAccount.CreateCloudTableClient();
        static readonly CloudTable Table = TableClient.GetTableReference("orders");

        readonly OrderJsonSerializer _orderJsonSerializer = new OrderJsonSerializer();
        
        public AzureTableStorageOrderStorage()
        {
            // Create the table if it doesn't exist.
            Table.CreateIfNotExists();
        }

        //http://azure.microsoft.com/en-us/documentation/articles/storage-dotnet-how-to-use-tables/#programmatically-access-table-storage
        public void Store(Order order)
        {
            // Create a new order entity.
            string partitionKey  = order.Customer.No.ToString(CultureInfo.InvariantCulture);
            string rowKey = order.OrderNo.ToString(CultureInfo.InvariantCulture);
            string orderAsJson = _orderJsonSerializer.Serialize(order);
            DynamicTableEntity orderEntity = new DynamicTableEntity(partitionKey, rowKey);
            Dictionary<string, EntityProperty> properties = new Dictionary<string, EntityProperty>
            {
                {"Order", new EntityProperty(orderAsJson)}
            };
            orderEntity.Properties = properties;

            // Create the TableOperation that inserts the customer entity.
            TableOperation insertOperation = TableOperation.Insert(orderEntity);

            // Execute the insert operation.
            TableResult result = Table.Execute(insertOperation);

            if (result.HttpStatusCode >= 400)
            {
                Trace.TraceInformation("Issue storing in storing order no. {0} was {1} : {2}.", order.OrderNo, result.HttpStatusCode, result.Result);
            }
        }
    }
}
