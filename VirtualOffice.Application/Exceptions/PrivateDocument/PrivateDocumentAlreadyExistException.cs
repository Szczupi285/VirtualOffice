using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Application.Exceptions.PrivateDocument
{
    public class PrivateDocumentAlreadyExistException : VirtualOfficeException
    {
        public PrivateDocumentAlreadyExistException(Guid id) : base($"Private document with Id: {id} already exist")
        {
        }
    }
}
