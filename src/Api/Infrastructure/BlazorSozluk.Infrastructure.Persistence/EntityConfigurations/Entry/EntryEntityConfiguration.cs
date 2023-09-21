using BlazorSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EntryModel = BlazorSozluk.Api.Domain.Models.Entry;

namespace BlazorSozluk.Infrastructure.Persistence.EntityConfigurations.Entry;

public class EntryEntityConfiguration : BaseEntityConfiguration<EntryModel>
{
    public override void Configure(EntityTypeBuilder<EntryModel> builder)
    {
        base.Configure(builder);

        builder.ToTable("entry", BlazorSozlukContext.DEFAULT_SCHEMA);

        builder.HasOne(i => i.CreatedBy)
            .WithMany(i => i.Entries)
            .HasForeignKey(i => i.CreatedById);
    }
}
