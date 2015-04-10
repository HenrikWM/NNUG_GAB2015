namespace GAB.Domain
{
    public class Customer
    {
        public int No { get; set; }

        public string Name { get; set; }

        public Customer()
        {
            No = 0;

            Name = string.Empty;
        }
    }
}