using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Note
{
    public class TooLongNoteTitleException : VirtualOfficeException
    {
        string Value;
        public TooLongNoteTitleException(string value, ushort length) : base($"Note Title: {value} is more than {length} characters long")
        {
            Value = value;
        }
    }
}
