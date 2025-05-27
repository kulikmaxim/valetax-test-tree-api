namespace ValetaxTestTree.Infrastructure.Messaging.Models
{
    public class BusMessage<T>
    {
        public long Id { get; }
        public T Payload { get; }

        public BusMessage(long id, T payload)
        {
            Id = id;
            Payload = payload;
        }
    }
}
