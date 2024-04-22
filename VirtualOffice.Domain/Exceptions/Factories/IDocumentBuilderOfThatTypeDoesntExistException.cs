using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Consts;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Factories
{
    public class IDocumentBuilderOfThatTypeDoesntExistException : VirtualOfficeException
    {
        DocumentTypeEnum Value;
        public IDocumentBuilderOfThatTypeDoesntExistException(DocumentTypeEnum value) : base($"IDocumentBuilder of type {value} is not supported")
        {
            Value = value;
        }
    }
}
