using System.Linq.Expressions;
using ValetaxTestTree.Domain.Entities;

namespace ValetaxTestTree.Domain.Repositories
{
    public interface IReadOnlyRepository<TEntity> where TEntity : class, IEntity
    {
        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default);
        public Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken = default);
    }
}
