using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ValetaxTestTree.Application.Requests;
using ValetaxTestTree.Infrastructure.Extensions;
using ValetaxTestTree.Infrastructure.Messaging;
using ValetaxTestTree.Infrastructure.Messaging.Consumers;
using ValetaxTestTree.Infrastructure.Messaging.Models;

namespace ValetaxTestTree.Api.BackgroundWorkers
{
    public class MessageBusListener : BackgroundService
    {
        private readonly IChannel channel;
        private readonly IJournalEventMessageConsumer journalEventMessageConsumer;

        public MessageBusListener(
            IMessageBusConnectionProvider busConnectionProvider,
            IJournalEventMessageConsumer journalEventMessageConsumer)
        {
            ArgumentNullException.ThrowIfNull(busConnectionProvider, nameof(busConnectionProvider));

            this.channel = busConnectionProvider.Channel;
            this.journalEventMessageConsumer = journalEventMessageConsumer
                ?? throw new ArgumentNullException(nameof(journalEventMessageConsumer));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += journalEventMessageConsumer.ConsumeAsync<CreateJournalEventCommand>;

            await channel.BasicConsumeAsync(
                queue: Queue.AddJournalEvent.GetDescription(),
                autoAck: false,
                consumer);
        }
    }
}
