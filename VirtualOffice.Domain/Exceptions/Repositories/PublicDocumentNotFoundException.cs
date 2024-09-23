using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Repositories
{
    public class PublicDocumentNotFoundException : VirtualOfficeException
    {
        public PublicDocumentNotFoundException(Guid guid) : base($"Public document with Id: {guid} has not been found")
        {
        }
    }
}