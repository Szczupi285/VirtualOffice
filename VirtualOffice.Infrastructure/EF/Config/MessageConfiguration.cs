using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.Message;

namespace VirtualOffice.Infrastructure.EF.Config
{
    internal sealed class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasConversion(
                p => p.Value,
                p => new MessageId(p));

            builder.Property(e => e.Content).HasConversion(
                p => p.Value,
                p => new MessageContent(p));

            builder.HasOne(e => e.Sender)
            .WithMany()
            .HasForeignKey("SentByUserId")
            .IsRequired();
        }
    }
}