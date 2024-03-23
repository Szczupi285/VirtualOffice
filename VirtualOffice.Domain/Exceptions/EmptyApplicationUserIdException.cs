using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions
{
    public class EmptyApplicationUserIdException : VirtualOfficeException
    {
        public EmptyApplicationUserIdException() : base("ApplicationUser Id cannot be empty")
        {
        }
    }
}
