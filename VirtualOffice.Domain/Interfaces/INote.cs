using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.Note;

namespace VirtualOffice.Domain.Interfaces
{
    public interface INote
    {
        NoteContent _content { get; }
        NoteTitle _title { get; }
        ApplicationUser _user { get; }

        void EditContent(string content);
        void EditTitle(string title);
    }
}