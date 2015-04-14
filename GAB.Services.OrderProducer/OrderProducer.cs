namespace GAB.Services.OrderProducer
{
    using System;

    using GAB.Domain;

    public class OrderProducer
    {
        public Order Produce(
            string orderNo,
            string customerNo,
            string customerName,
            string orderItemNo,
            string orderItemName)
        {
            return new Order {
                OrderNo = int.Parse(orderNo),
                Created = DateTime.Now,
                Customer = new Customer { No = int.Parse(customerNo), Name = customerName },
                OrderItem = new OrderItem { No = int.Parse(orderItemNo), Name = orderItemName }
            };
        }
    }
}