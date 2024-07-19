using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Application.Exceptions.PublicDocument
{
    public class PublicDocumentDoesNotExistException : VirtualOfficeException
    {
        public PublicDocumentDoesNotExistException(Guid id) : base($"Public document with Id: {id} does not exist")
        {
        }
    }
}
