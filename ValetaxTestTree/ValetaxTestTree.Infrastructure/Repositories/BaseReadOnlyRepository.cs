using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ValetaxTestTree.Domain.Entities;
using ValetaxTestTree.Domain.Repositories;

namespace ValetaxTestTree.Infrastructure.Repositories
{
    public class BaseReadOnlyRepository<TEntity> : IReadOnlyRepository<TEntity>
        where TEntity : class, IEntity
    {
        protected AppDbContext Context { get; }
        protected DbSet<TEntity> DbSet { get; }

        protected BaseReadOnlyRepository(AppDbContext appDbContext)
        {
            Context = appDbContext;
            DbSet = appDbContext.Set<TEntity>();
        }

        protected IQueryable<TEntity> GetQuery(QueryOptions options = null)
        {
            var query = DbSet.AsQueryable();
            if (options == null)
            {
                return query;
            }

            if (options.AsNoTracking)
            {
                query = query.AsNoTracking();
            }

            if (options.AsSplitQuery)
            {
                query = query.AsSplitQuery();
            }

            return query;
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken = default)
        {
            return await GetQuery().AnyAsync(expression, cancellationToken);
        }

        public async Task<TEntity> FindAsync(object[] keyValues, CancellationToken cancellationToken = default)
        {
            return await DbSet.FindAsync(keyValues, cancellationToken);
        }
    }
}
