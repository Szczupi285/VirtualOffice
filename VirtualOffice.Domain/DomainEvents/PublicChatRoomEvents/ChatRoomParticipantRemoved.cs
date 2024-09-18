using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Domain.DomainEvents.PublicChatRoomEvents
{
    public record ChatRoomParticipantRemoved(PublicChatRoom room, ApplicationUser participant) : IDomainEvent;
}