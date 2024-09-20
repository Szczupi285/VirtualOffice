using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.AbstractChatRoom;

namespace VirtualOffice.Infrastructure.EF.Config
{
    internal sealed class PrivateChatRoomConfiguration : IEntityTypeConfiguration<PrivateChatRoom>
    {
        public void Configure(EntityTypeBuilder<PrivateChatRoom> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasConversion(
                p => p.Value,
                p => new ChatRoomId(p));

            builder.HasMany(e => e._Participants)
             .WithMany();

            builder.HasMany(e => e._Messages)
            .WithMany();
        }
    }
}