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