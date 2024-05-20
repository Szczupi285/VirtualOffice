using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.DomainEvents.NoteEvent;
using VirtualOffice.Domain.ValueObjects.Note;

namespace VirtualOffice.Domain.Entities
{
    public class Note : AggregateRoot<NoteId>
    {
        public NoteTitle _title { get; private set; }

        public NoteContent _content { get; private set; }

        public Note(NoteId id, NoteTitle title, NoteContent content)
        {
            Id = id;
            _title = title;
            _content = content;
        }

        public void EditContent(string content)
        {
            _content = content;
            AddEvent(new NoteContentChanged(this));
        }
        public void EditTitle(string title)
        {
            _title = title;
            AddEvent(new NoteTitleChanged(this));
        }

    }
}
