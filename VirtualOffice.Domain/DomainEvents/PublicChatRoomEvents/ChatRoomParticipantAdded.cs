using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Domain.DomainEvents.PublicChatRoomEvents
{
    public record ChatRoomParticipantAdded(PublicChatRoom room, ApplicationUser participant) : IDomainEvent;
}