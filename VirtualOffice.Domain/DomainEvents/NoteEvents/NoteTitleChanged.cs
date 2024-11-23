using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Domain.DomainEvents.NoteEvent
{
    public record NoteTitleChanged(Note note) : IDomainEvent;
}