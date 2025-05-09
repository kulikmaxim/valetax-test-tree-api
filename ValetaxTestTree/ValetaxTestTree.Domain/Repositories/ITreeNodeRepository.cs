using ValetaxTestTree.Domain.Entities;

namespace ValetaxTestTree.Domain.Repositories
{
    public interface ITreeNodeRepository : IReadWriteRepository<TreeNode>
    {
        public Task<ICollection<TreeNode>> GetTreeHierarchyAsync(string nodeName, CancellationToken cancellationToken = default);
    }
}
