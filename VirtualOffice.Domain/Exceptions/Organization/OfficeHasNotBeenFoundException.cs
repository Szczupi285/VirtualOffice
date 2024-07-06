using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Organization
{
    public class OfficeHasNotBeenFoundException : VirtualOfficeException
    {
        Guid Value;
        public OfficeHasNotBeenFoundException(Guid value) : base($"Office with id: {value} has not been found")
        {
            Value = value;
        }
    }
}
