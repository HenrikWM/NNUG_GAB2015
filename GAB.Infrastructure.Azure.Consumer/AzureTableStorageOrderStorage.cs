namespace GAB.Infrastructure.Azure.Consumer
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;

    using GAB.Core;

    using Microsoft.Azure;
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Table;

    public class AzureTableStorageOrderStorage
    {
        public int GetTotalNumberOfOrders()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference(TableStorageConstants.OrdersTableName);

            TableQuery<OrderEntity> query = new TableQuery<OrderEntity>();

            IEnumerable<OrderEntity> nn = table.ExecuteQuery(query);

            return nn.Count();
        }

        private class OrderEntity : TableEntity
        {
            
        }
    }
}
