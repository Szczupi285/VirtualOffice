using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Exceptions.Note;

namespace VirtualOffice.Domain.ValueObjects.Note
{
    public sealed record NoteContent : AbstractRecordName
    {
        public NoteContent(string value) : base(value, 1000, new EmptyNoteContentException(), new TooLongNoteContentException(value, 1000))
        {
        }

        public static implicit operator NoteContent(string content)
            => new(content);
    }
}