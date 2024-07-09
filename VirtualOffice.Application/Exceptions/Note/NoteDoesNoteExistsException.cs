using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Application.Exceptions.Note
{
    public class NoteDoesNoteExistsException : VirtualOfficeException
    {
        public NoteDoesNoteExistsException(Guid id) : base($"Note with Id: {id} does not exist")
        {
        }
    }
}
