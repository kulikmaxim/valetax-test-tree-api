using RabbitMQ.Client;

namespace ValetaxTestTree.Infrastructure.Messaging
{
    public class RabbitMqConnectionProvider : IAsyncDisposable, IMessageBusConnectionProvider
    {
        // TODO: Add logic channel/connection re-creating in case failure
        public IConnection Connection { get; protected set; }
        public IChannel Channel { get; protected set; }

        public RabbitMqConnectionProvider(string hostName)
        {
            InitAsync(hostName).Wait();
        }

        private async Task InitAsync(string hostName)
        {
            var factory = new ConnectionFactory
            {
                HostName = hostName,
            };

            Connection = await factory.CreateConnectionAsync();
            Channel = await Connection.CreateChannelAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await Channel.CloseAsync();
            await Connection.CloseAsync();
            await Channel.DisposeAsync();
            await Connection.DisposeAsync();

            GC.SuppressFinalize(this);
        }
    }
}
