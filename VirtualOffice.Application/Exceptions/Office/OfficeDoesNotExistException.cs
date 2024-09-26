using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Application.Exceptions.Office
{
    public class OfficeDoesNotExistException : VirtualOfficeException
    {
        public OfficeDoesNotExistException(Guid guid) : base($"Office with id {guid} does not exist")
        {
        }
    }
}
