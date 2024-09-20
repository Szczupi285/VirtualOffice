using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.AbstractChatRoom;
using VirtualOffice.Domain.ValueObjects.ChatRoom;

namespace VirtualOffice.Infrastructure.EF.Config
{
    internal sealed class PublicChatRoomConfiguration : IEntityTypeConfiguration<PublicChatRoom>
    {
        public void Configure(EntityTypeBuilder<PublicChatRoom> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasConversion(
                p => p.Value,
                p => new ChatRoomId(p));

            builder.Property(e => e._Name).HasConversion(
                p => p.Value,
                p => new PublicChatRoomName(p));

            builder.HasMany(e => e._Participants)
             .WithMany();

            builder.HasMany(e => e._Messages)
            .WithMany();
        }
    }
}