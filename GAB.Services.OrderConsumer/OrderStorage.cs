namespace GAB.Services.OrderConsumer
{
    using GAB.Infrastructure.Azure.Consumer;

    public class OrderStorage
    {
        readonly AzureTableStorageOrderStorage azureTableStorageOrderStorage = new AzureTableStorageOrderStorage();

        public int GetTotalNumberOfOrders()
        {
            return azureTableStorageOrderStorage.GetTotalNumberOfOrders();
        }
    }
}