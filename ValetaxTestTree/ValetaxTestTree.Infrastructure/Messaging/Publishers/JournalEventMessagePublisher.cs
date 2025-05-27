using Microsoft.Extensions.Logging;
using ValetaxTestTree.Application.Requests;
using ValetaxTestTree.Infrastructure.Messaging.Models;

namespace ValetaxTestTree.Infrastructure.Messaging.Publishers
{
    public class JournalEventMessagePublisher : BaseMessagePublisher, IJournalEventMessagePublisher
    {
        public JournalEventMessagePublisher(IMessageBusConnectionProvider busConnectionProvider, ILogger<BaseMessagePublisher> logger) : base(busConnectionProvider, logger)
        {
        }

        public async Task PublishJournalEventAsync(CreateJournalEventCommand command)
        {
            ArgumentNullException.ThrowIfNull(command, nameof(command));

            var message = new BusMessage<CreateJournalEventCommand>(command.EventId, command);
            var messageBusInfo = new MessageBusInfo(
                Exchange.JournalEvent,
                Queue.AddJournalEvent);

            await SendMessageAsync(message, messageBusInfo);
        }
    }
}
