using MediatR;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace ValetaxTestTree.Infrastructure.Messaging.Consumers
{
    public class JournalEventMessageConsumer : BaseMessageConsumer, IJournalEventMessageConsumer
    {
        private readonly IMediator mediator;
        private readonly IChannel channel;

        public JournalEventMessageConsumer(
            ILogger<JournalEventMessageConsumer> logger,
            IMediator mediator,
            IMessageBusConnectionProvider busConnectionProvider) 
            : base(logger)
        {
            ArgumentNullException.ThrowIfNull(busConnectionProvider, nameof(busConnectionProvider));

            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            channel = busConnectionProvider.Channel;
        }

        protected override async Task HandlePayloadAsync<CreateJournalEventCommand>(
            CreateJournalEventCommand createCommand,
            BasicDeliverEventArgs args)
        {
            await mediator.Send(createCommand);
            await channel.BasicAckAsync(args.DeliveryTag, false);
        }
    }
}
