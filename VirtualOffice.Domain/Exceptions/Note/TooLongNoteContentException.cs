using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Note
{
    public class TooLongNoteContentException : VirtualOfficeException
    {
        string Value;
        public TooLongNoteContentException(string value, ushort length) : base($"Note Content: {value} is more than {length} characters long")
        {
            Value = value;
        }
    }
}
