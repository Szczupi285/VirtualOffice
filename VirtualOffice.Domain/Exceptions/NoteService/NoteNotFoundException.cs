using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.NoteService
{
    public class NoteNotFoundException : VirtualOfficeException
    {
        Guid Value;
        public NoteNotFoundException(Guid value) : base($"Note with Id: {value} has not been found")
        {
            Value = value;
        }
    }
}
