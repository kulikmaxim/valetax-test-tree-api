using Microsoft.EntityFrameworkCore;
using ValetaxTestTree.Domain.Entities;
using ValetaxTestTree.Infrastructure.ModelConfigurations;

namespace ValetaxTestTree.Infrastructure.Storage
{
    public class AppDbContext : DbContext
    {
        public DbSet<TreeNode> TreeNodes { get; set; }
        public DbSet<JournalEvent> JournalEvents { get; set; }

        public AppDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(TreeNodeConfiguration).Assembly);

            modelBuilder
                .HasDbFunction(() => GetTreeHierarchy(default, default))
                .HasName("get_tree_hierarchy");
        }

        public IQueryable<TreeNode> GetTreeHierarchy(string nodeName, int maxLevel) =>
            FromExpression(() => GetTreeHierarchy(nodeName, maxLevel));
    }
}
