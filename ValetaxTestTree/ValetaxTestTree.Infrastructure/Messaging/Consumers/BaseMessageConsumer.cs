using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client.Events;
using ValetaxTestTree.Infrastructure.Messaging.Models;

namespace ValetaxTestTree.Infrastructure.Messaging.Consumers
{
    public abstract class BaseMessageConsumer
    {
        protected ILogger<JournalEventMessageConsumer> Logger { get; }

        protected BaseMessageConsumer(ILogger<JournalEventMessageConsumer> logger)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected abstract Task HandlePayloadAsync<T>(T payload, BasicDeliverEventArgs args);

        public async Task ConsumeAsync<T>(object sender, BasicDeliverEventArgs args)
        {
            long? messageId = null;
            try
            {
                byte[] body = args.Body.ToArray();
                var stringBody = Encoding.UTF8.GetString(body);
                var message = JsonSerializer.Deserialize<BusMessage<T>>(stringBody);
                messageId = message.Id;

                await HandlePayloadAsync(message.Payload, args);

                Logger.LogInformation($"Message '{messageId}' handled successfully");
            }
            catch (Exception e)
            {
                Logger.LogError(e, $"Message '{messageId}' handling failed");
                throw;
            }
        }
    }
}
