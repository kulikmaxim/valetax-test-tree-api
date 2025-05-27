using Microsoft.EntityFrameworkCore;
using ValetaxTestTree.Domain.Entities;
using ValetaxTestTree.Domain.Repositories;
using ValetaxTestTree.Infrastructure.Storage;

namespace ValetaxTestTree.Infrastructure.Repositories
{
    public class TreeNodeRepository : BaseReadWriteRepository<TreeNode>, ITreeNodeRepository
    {
        private const int MaxTreeLevel = 50;
        public TreeNodeRepository(AppDbContext appDbContext) : base(appDbContext)
        {
        }

        public async Task<ICollection<TreeNode>> GetTreeHierarchyAsync(string nodeName, CancellationToken cancellationToken = default)
        {
            var treeHierarchy = await Context.GetTreeHierarchy(nodeName, MaxTreeLevel)
                .AsNoTrackingWithIdentityResolution()
                .ToListAsync(cancellationToken);

            return treeHierarchy;
        }
    }
}
