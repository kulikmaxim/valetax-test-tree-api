using ValetaxTestTree.Domain.Entities;

namespace ValetaxTestTree.Domain.Repositories
{
    public interface IReadWriteRepository<TEntity> : IReadOnlyRepository<TEntity>
        where TEntity : class, IEntity
    {
        public TEntity Add(TEntity entity);

        public TEntity Remove(TEntity entity);

        public TEntity Update(TEntity entity);

        public Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
