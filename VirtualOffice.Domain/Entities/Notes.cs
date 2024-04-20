using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.ValueObjects.Notes;

namespace VirtualOffice.Domain.Entities
{
    public class Notes
    {
        public NoteId Id { get; private set; }

        public NoteTitle _title { get; private set; }

        public NoteContent _content { get; private set; }

        public Notes(NoteId id, NoteTitle title, NoteContent content)
        {
            Id = id;
            _title = title;
            _content = content;
        }
    }
}
