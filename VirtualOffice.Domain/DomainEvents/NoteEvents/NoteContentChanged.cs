using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.Note;

namespace VirtualOffice.Domain.DomainEvents.NoteEvent
{
    public record NoteContentChanged(Note note, NoteContent content) : IDomainEvent;
}