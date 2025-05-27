using RabbitMQ.Client;

namespace ValetaxTestTree.Infrastructure.Messaging
{
    public interface IMessageBusConnectionProvider
    {
        public IConnection Connection { get; }
        public IChannel Channel { get; }
    }
}
