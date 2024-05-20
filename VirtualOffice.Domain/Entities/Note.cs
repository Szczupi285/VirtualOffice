using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.ValueObjects.Note;

namespace VirtualOffice.Domain.Entities
{
    public class Note
    {
        public NoteId Id { get; }

        public NoteTitle _title { get; private set; }

        public NoteContent _content { get; private set; }

        public Note(NoteId id, NoteTitle title, NoteContent content)
        {
            Id = id;
            _title = title;
            _content = content;
        }
        public void EditContent(string content) => _content = content;

        public void EditTitle(string title) => _title = title;

    }
}
