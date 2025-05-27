namespace ValetaxTestTree.Infrastructure.Messaging.Models
{
    public class MessageBusInfo
    {
        public Exchange Exchange { get; }
        public Queue Queue { get; }

        public MessageBusInfo(Exchange exchange, Queue queue)
        {
            Exchange = exchange;
            Queue = queue;
        }
    }
}
