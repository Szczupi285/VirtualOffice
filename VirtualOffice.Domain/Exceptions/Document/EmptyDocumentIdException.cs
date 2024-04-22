using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Document
{
    public class EmptyDocumentIdException : VirtualOfficeException
    {
        public EmptyDocumentIdException() 
            : base("Document Id cannot be empty")
        {
        }
    }
}
