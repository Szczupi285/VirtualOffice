using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Office
{
    public class EmptyOfficeIdException : VirtualOfficeException
    {
        public EmptyOfficeIdException() : base("Office Id cannot be empty")
        {
        }
    }
}