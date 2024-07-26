using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Application.Exceptions.Note
{
    public class NoteAlreadayExistsException : VirtualOfficeException
    {
        public NoteAlreadayExistsException(Guid guid) : base($"Note with Id: {guid} already exsits")
        {
        }
    }
}
