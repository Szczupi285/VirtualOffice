using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.Note;

namespace VirtualOffice.Infrastructure.EF.Config
{
    internal sealed class NoteConfiguration : IEntityTypeConfiguration<Note>
    {
        public void Configure(EntityTypeBuilder<Note> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasConversion(
                p => p.Value,
                p => new NoteId(p));

            builder.Property(e => e._title).HasConversion(
                p => p.Value,
                p => new NoteTitle(p));

            builder.Property(e => e._content).HasConversion(
                p => p.Value,
                p => new NoteContent(p));

            builder.Property(e => e.Version)
            .IsConcurrencyToken();

            builder.HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(e => e._createdBy)
            .IsRequired();
        }
    }
}