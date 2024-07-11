using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Application.Exceptions.PrivateDocument
{
    public class PrivateDocumentAlreadyDoeasNotExistException : VirtualOfficeException
    {
        public PrivateDocumentAlreadyDoeasNotExistException(Guid id) : base($"Private document with Id: {id} does not exist")
        {
        }
    }
}
