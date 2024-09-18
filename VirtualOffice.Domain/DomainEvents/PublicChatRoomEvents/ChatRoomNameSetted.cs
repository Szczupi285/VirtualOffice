using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ChatRoom;

namespace VirtualOffice.Domain.DomainEvents.PublicChatRoomEvents
{
    public record ChatRoomNameSetted(PublicChatRoom room, PublicChatRoomName name) : IDomainEvent;
}