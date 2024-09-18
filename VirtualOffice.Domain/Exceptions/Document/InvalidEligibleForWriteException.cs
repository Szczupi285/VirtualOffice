using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Document
{
    public class InvalidEligibleForWriteException : VirtualOfficeException
    {
        public InvalidEligibleForWriteException()
            : base("There must be at least one person eligible for write")
        {
        }
    }
}