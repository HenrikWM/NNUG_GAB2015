namespace GAB.Core
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    [DataContract]   
    public class Order
    {
        [DataMember(Name = "customer")]
        public Customer Customer { get; set; }

        [DataMember(Name = "orderItem")]
        public OrderItem OrderItem { get; set; }

        [DataMember(Name = "created")]
        public DateTime Created { get; set; }

        [DataMember(Name = "orderNo")]
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
