using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.DomainEvents.NoteEvent;
using VirtualOffice.Domain.DomainEvents.NoteEvents;
using VirtualOffice.Domain.ValueObjects.Note;

namespace VirtualOffice.Domain.Entities
{
    public class Note : AggregateRoot<NoteId>
    {
        public NoteTitle _title { get; private set; }

        public NoteContent _content { get; private set; }

        public ApplicationUser _createdBy { get; }

        public Note(NoteId id, NoteTitle title, NoteContent content, ApplicationUser user)
        {
            Id = id;
            _title = title;
            _content = content;
            _createdBy = user;
            AddEvent(new NoteCreated(id, title, content, user));
        }

        private Note()
        { }

        public void EditContent(string content)
        {
            _content = content;
            AddEvent(new NoteContentChanged(this, content));
        }

        public void EditTitle(string title)
        {
            _title = title;
            AddEvent(new NoteTitleChanged(this, title));
        }
    }
}