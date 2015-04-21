namespace GAB.Services.OrderConsumer
{
    using GAB.Core;

    public interface IOrderStorage
    {
        int GetTotalNumberOfOrders();

        void Store(Order order);
    }
}