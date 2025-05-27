using ValetaxTestTree.Application.Requests;

namespace ValetaxTestTree.Infrastructure.Messaging.Publishers
{
    public interface IJournalEventMessagePublisher
    {
        public Task PublishJournalEventAsync(CreateJournalEventCommand command);
    }
}
