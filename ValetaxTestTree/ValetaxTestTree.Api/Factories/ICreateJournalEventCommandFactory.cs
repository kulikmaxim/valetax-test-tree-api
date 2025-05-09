using ValetaxTestTree.Application.Requests;

namespace ValetaxTestTree.Api.Factories
{
    public interface ICreateJournalEventCommandFactory
    {
        public Task<CreateJournalEventCommand> CreateAsync(HttpContext context, Exception exception);
    }
}
