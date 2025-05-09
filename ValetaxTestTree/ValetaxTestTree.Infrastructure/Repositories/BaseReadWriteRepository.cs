using ValetaxTestTree.Domain.Entities;
using ValetaxTestTree.Domain.Repositories;

namespace ValetaxTestTree.Infrastructure.Repositories
{
    public class BaseReadWriteRepository<TEntity> : BaseReadOnlyRepository<TEntity>, IReadWriteRepository<TEntity>
        where TEntity : class, IEntity
    {
        public BaseReadWriteRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public TEntity Add(TEntity entity)
        {
            var entry = DbSet.Add(entity);

            return entry.Entity;
        }

        public TEntity Remove(TEntity entity)
        {
            var entry = DbSet.Remove(entity);

            return entry.Entity;
        }

        public TEntity Update(TEntity entity)
        {
            var entry = DbSet.Update(entity);

            return entry.Entity;
        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}
