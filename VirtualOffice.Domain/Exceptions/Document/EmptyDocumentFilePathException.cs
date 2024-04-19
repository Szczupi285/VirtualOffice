using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Document
{
    public class EmptyDocumentFilePathException : VirtualOfficeException
    {
        string Value;
        public EmptyDocumentFilePathException(string value) : base($"DocumentFilePath: '{value}' cannot be empty")
        {
            Value = value;
        }
    }
}
