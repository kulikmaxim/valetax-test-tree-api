using Microsoft.EntityFrameworkCore;
using ValetaxTestTree.Domain.Entities;
using ValetaxTestTree.Domain.Models;
using ValetaxTestTree.Domain.Repositories;

namespace ValetaxTestTree.Infrastructure.Repositories
{
    public class JournalEventRepository : BaseReadWriteRepository<JournalEvent>, IJournalEventRepository
    {
        const int MaxItemsCount = 1000;

        public JournalEventRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<PageResult<JournalEvent>> GetAsync(
            BaseFilter<JournalEventCriteria> filter,
            CancellationToken cancellationToken = default)
        {
            var query = GetByCriteria(filter.Criteria);

            var count = await query.CountAsync(cancellationToken);

            var take = Math.Min(filter.Take, MaxItemsCount);
            var items = await query
                .Skip(filter.Skip)
                .Take(take)
                .ToListAsync(cancellationToken);

            var result = new PageResult<JournalEvent>
            {
                Skip = filter.Skip,
                Count = count,
                Items = items
            };

            return result;
        }

        private IQueryable<JournalEvent> GetByCriteria(JournalEventCriteria criteria)
        {
            var query = GetQuery(new QueryOptions(asNoTracking: true));
            if (criteria == null)
                return query;

            if (criteria.From.HasValue)
                query = query.Where(x => x.Timestamp >= criteria.From);
            if (criteria.To.HasValue)
                query = query.Where(x => x.Timestamp <= criteria.To);
            if (!string.IsNullOrWhiteSpace(criteria.Search))
                query = query.Where(x => x.Info.Contains(criteria.Search));

            return query;
        }
    }
}
