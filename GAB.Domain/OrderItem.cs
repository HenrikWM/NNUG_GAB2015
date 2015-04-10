namespace GAB.Domain
{
    public class OrderItem
    {
        public int No { get; set; }

        public string Name { get; set; }

        public OrderItem()
        {
            No = 0;

            Name = string.Empty;
        }
    }
}