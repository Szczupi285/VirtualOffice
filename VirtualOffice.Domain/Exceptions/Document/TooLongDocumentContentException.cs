using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Document
{
    public class TooLongDocumentContentException : VirtualOfficeException
    {
        string Value;
        public TooLongDocumentContentException(string value, uint length) : base($"Document Content: {value} is more than {length} characters long")
        {
            Value = value;
        }
    }
}
