using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Document
{
    public class TooLongDocumentTitleException : VirtualOfficeException
    {
        string Value;
        public TooLongDocumentTitleException(string value) : base($"Title: {value} is more than 50 characters long")
        {
            Value = value; 
        }
    }
}
