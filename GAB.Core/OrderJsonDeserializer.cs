namespace GAB.Core
{
    using Newtonsoft.Json;

    public class OrderJsonDeserializer
    {
        public Order Deserialize(string text)
        {
            return JsonConvert.DeserializeObject<Order>(text);
        }
    }
}