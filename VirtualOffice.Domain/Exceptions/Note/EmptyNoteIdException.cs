using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Note
{
    public class EmptyNoteIdException : VirtualOfficeException
    {
        public EmptyNoteIdException()
            : base("Note Id cannot be empty")
        {
        }
    }
}
