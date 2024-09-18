using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Domain.DomainEvents.AbstractChatRoomEvents
{
    public record ChatRoomMessageSent(AbstractChatRoom room, Message message) : IDomainEvent;
}