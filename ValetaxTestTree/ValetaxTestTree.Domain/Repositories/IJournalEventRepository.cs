using ValetaxTestTree.Domain.Entities;
using ValetaxTestTree.Domain.Models;

namespace ValetaxTestTree.Domain.Repositories
{
    public interface IJournalEventRepository : IReadWriteRepository<JournalEvent>
    {
        public Task<PageResult<JournalEvent>> GetAsync(
            BaseFilter<JournalEventCriteria> filter,
            CancellationToken cancellationToken = default);
    }
}
