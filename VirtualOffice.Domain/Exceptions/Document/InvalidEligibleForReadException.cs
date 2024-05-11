using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Document
{
    public class InvalidEligibleForReadException : VirtualOfficeException
    {
        public InvalidEligibleForReadException() 
            : base("There must be at least one person eligible for write")
        {
        }
    }
}
