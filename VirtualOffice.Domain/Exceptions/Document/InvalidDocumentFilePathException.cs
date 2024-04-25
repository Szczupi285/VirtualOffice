using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Document
{
    public class NotAValidDocumentFilePathException : VirtualOfficeException
    {
        string Value;
        public NotAValidDocumentFilePathException(string value) : base($"File path :{value} is not a valid windows server File Path")
        {
            Value = value;
        }
    }
}
