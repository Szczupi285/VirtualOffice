using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Exceptions.Note;

namespace VirtualOffice.Domain.ValueObjects.Note
{
    public sealed record NoteId : AbstractRecordId
    {
        public NoteId(Guid value) : base(value, new EmptyNoteIdException())
        {
        }

        public static implicit operator NoteId(Guid id)
            => new(id);
    }
}