namespace GAB.Core
{
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class OrderJsonSerializer
    {
        public string Serialize(Order order)
        {
            return JsonConvert.SerializeObject(order);
        }

        public string Serialize(List<Order> orders)
        {
            return JsonConvert.SerializeObject(orders);
        }
    }
}