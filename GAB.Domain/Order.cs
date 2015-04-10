namespace GAB.Domain
{
    using System;

    public class Order
    {
        public Customer Customer { get; set; }

        public OrderItem OrderItem { get; set; }

        public DateTime Created { get; set; }

        public int OrderNo { get; set; }

        public Order()
        {
            Customer = new Customer();
            
            OrderItem = new OrderItem();

            Created = DateTime.MinValue;

            OrderNo = 0;
        }
    }
}
