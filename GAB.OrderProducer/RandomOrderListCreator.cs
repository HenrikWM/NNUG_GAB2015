namespace GAB.OrderProducer
{
    using System.Collections.Generic;

    using FizzWare.NBuilder;

    using GAB.Domain;

    public class RandomOrderListCreator
    {
        public IList<Order> Create(int size)
        {
            UniqueRandomGenerator uniqueRandomGenerator = new UniqueRandomGenerator();

            return
                Builder<Order>.CreateListOfSize(size)
                              .All()
                              .With(o => o.OrderNo = uniqueRandomGenerator.Next(0, 999999999))
                              .With(o => o.Customer.No = uniqueRandomGenerator.Next(0, 999999999))
                              .With(o => o.Customer.Name = RandomStringGenerator.GetRandomString())
                              .With(o => o.OrderItem.No = uniqueRandomGenerator.Next(0, 999999999))
                              .With(o => o.OrderItem.Name = RandomStringGenerator.GetRandomString())
                              .Build();
        }
    }
}