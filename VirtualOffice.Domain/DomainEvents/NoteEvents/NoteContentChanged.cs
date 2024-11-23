using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Domain.DomainEvents.NoteEvent
{
    public record NoteContentChanged(Note note) : IDomainEvent;
}