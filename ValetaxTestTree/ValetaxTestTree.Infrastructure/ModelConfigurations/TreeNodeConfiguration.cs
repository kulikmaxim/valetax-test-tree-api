using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ValetaxTestTree.Domain.Entities;

namespace ValetaxTestTree.Infrastructure.ModelConfigurations
{
    public class TreeNodeConfiguration : IEntityTypeConfiguration<TreeNode>
    {
        public void Configure(EntityTypeBuilder<TreeNode> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => new { x.ParentId, x.Name })
                .IsUnique()
                .AreNullsDistinct(false);
            builder.ToTable(x => x.HasCheckConstraint(
                "CK_TreeNode_Name_NotEmpty",
                "nullif(trim(\"Name\"),'') is not null"));
            builder
               .Property(x => x.Name)
               .IsRequired();
            builder.HasOne(x => x.Parent)
                .WithMany(x => x.Children)
                .HasForeignKey(x => x.ParentId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
