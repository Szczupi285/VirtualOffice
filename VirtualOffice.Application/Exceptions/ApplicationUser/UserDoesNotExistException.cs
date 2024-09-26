using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Application.Exceptions.ApplicationUser
{
    public class UserDoesNotExistException : VirtualOfficeException
    {
        public UserDoesNotExistException(Guid id) : base($"User with Id: {id} does not exist")
        {
        }
    }
}
