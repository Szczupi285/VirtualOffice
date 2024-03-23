using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions
{
    public class EmptyApplicationUserNameException : VirtualOfficeException
    {
        public EmptyApplicationUserNameException() : base("ApplicationUser name cannot be empty")
        {
        }
    }
}
