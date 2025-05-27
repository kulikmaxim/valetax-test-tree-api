using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using ValetaxTestTree.Infrastructure.Extensions;
using ValetaxTestTree.Infrastructure.Messaging.Models;

namespace ValetaxTestTree.Infrastructure.Messaging.Publishers
{
    public abstract class BaseMessagePublisher
    {
        private readonly IChannel channel;
        private readonly ILogger<BaseMessagePublisher> logger;

        public BaseMessagePublisher(
            IMessageBusConnectionProvider busConnectionProvider,
            ILogger<BaseMessagePublisher> logger)
        {
            ArgumentNullException.ThrowIfNull(busConnectionProvider, nameof(busConnectionProvider));

            channel = busConnectionProvider.Channel;
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected async Task SendMessageAsync<T>(BusMessage<T> message, MessageBusInfo messageBusInfo) 
        {
            try
            {
                var json = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(json);

                await channel.BasicPublishAsync(
                    messageBusInfo.Exchange.GetDescription(),
                    messageBusInfo.Queue.GetDescription(),
                    body);

                logger.LogInformation($"Message '{message.Id}' published successfully");
            }
            catch (Exception e)
            {
                logger.LogError(e, $"Message '{message.Id}' publishing failed");
                throw;
            }
        }
    }
}
