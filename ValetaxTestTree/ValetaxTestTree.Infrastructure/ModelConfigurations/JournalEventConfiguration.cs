using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ValetaxTestTree.Domain.Entities;

namespace ValetaxTestTree.Infrastructure.ModelConfigurations
{
    public class JournalEventConfiguration : IEntityTypeConfiguration<JournalEvent>
    {
        public void Configure(EntityTypeBuilder<JournalEvent> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.EventId).IsRequired();
            builder.Property(x => x.Timestamp).IsRequired();
            builder.Property(x => x.Info).IsRequired();
        }
    }
}
