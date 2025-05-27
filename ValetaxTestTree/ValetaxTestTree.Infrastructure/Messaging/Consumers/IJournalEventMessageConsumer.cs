using RabbitMQ.Client.Events;

namespace ValetaxTestTree.Infrastructure.Messaging.Consumers
{
    public interface IJournalEventMessageConsumer
    {
        public Task ConsumeAsync<CreateJournalEventCommand>(object sender, BasicDeliverEventArgs args);
    }
}
