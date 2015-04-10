namespace GAB.Domain
{
    using Newtonsoft.Json;

    public class OrderJsonSerializer
    {
        public string Serialize(Order order)
        {
            return JsonConvert.SerializeObject(order);
        }
    }
}