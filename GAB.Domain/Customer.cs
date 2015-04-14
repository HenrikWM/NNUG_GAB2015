namespace GAB.Domain
{
    using System.Runtime.Serialization;

    [DataContract]
    public class Customer
    {
        [DataMember(Name = "no")]
        public int No { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        public Customer()
        {
            No = 0;

            Name = string.Empty;
        }
    }
}