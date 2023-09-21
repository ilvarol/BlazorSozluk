using BlazorSozluk.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EntryCommentEntity = BlazorSozluk.Api.Domain.Models.EntryComment;

namespace BlazorSozluk.Infrastructure.Persistence.EntityConfigurations.EntryComment;

public class EntryEntityConfiguration : BaseEntityConfiguration<EntryCommentEntity>
{
    public override void Configure(EntityTypeBuilder<EntryCommentEntity> builder)
    {
        base.Configure(builder);

        builder.ToTable("entrycomment", BlazorSozlukContext.DEFAULT_SCHEMA);

        builder.HasOne(i => i.CreatedBy)
            .WithMany(i => i.EntryComments)
            .HasForeignKey(i => i.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(i => i.Entry)
            .WithMany(i => i.EntryComments)
            .HasForeignKey(i => i.EntryId);
    }
}
