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
        public TooLongNoteContentException(string value) : base($"Note Content: {value} is more than 1000 characters long")
        {
            Value = value;
        }
    }
}
