namespace ValetaxTestTree.Domain.Entities
{
    public class TreeNode : IEntity
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Name { get; set; }
        public TreeNode Parent { get; set; }
        public ICollection<TreeNode> Children { get; set; }
    }
}
