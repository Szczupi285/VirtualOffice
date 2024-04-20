using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Exceptions.Note;

namespace VirtualOffice.Domain.ValueObjects.Notes
{
    public sealed record NoteTitle : AbstractRecordName
    {
        public NoteTitle(string value) : base(value, 60, new EmptyNoteTitleException(), new TooLongNoteTitleException(value, 60))
        {
        }

        public static implicit operator NoteTitle(string title)
            => new(title);
    }
}
