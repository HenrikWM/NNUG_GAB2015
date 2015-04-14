namespace GAB.Services.OrderProducer
{
    using GAB.Domain;

    public interface IOrderProducer
    {
        Order Produce(string orderNo, string customerNo, string customerName, string orderItemNo, string orderItemName);
    }
}