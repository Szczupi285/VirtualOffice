using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Domain.DomainEvents.NoteEvents
{
    public record NoteCreated(Guid Id, string Title, string Content, ApplicationUser User) : IDomainEvent;
}