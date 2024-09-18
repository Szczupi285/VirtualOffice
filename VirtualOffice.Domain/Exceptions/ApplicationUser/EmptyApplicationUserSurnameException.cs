using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.ApplicationUser
{
    public class EmptyApplicationUserSurnameException : VirtualOfficeException
    {
        public EmptyApplicationUserSurnameException() : base("ApplicationUser surname cannot be empty")
        {
        }
    }
}