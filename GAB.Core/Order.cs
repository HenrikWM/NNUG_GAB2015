namespace GAB.Core
{
    using System;
    using System.Globalization;
    using System.Runtime.Serialization;

    [Serializable]
    [DataContract]   
    public class Order
    {
        public string PartitionKey
        {
            get
            {
                return Customer.No.ToString(CultureInfo.InvariantCulture);
            }
        }

        public string RowKey
        {
            get
            {
                return OrderNo.ToString(CultureInfo.InvariantCulture);
            }
        }

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

        public override string ToString()
        {
            return string.Format("Customer: {0}. OrderItem: {1}. Created: {2}, OrderNo: {3}", Customer, OrderItem, Created, OrderNo);
        }
    }
}
