using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.ApplicationUser
{
    public class InvalidApplicationUserSurnameException : VirtualOfficeException
    {
        private string Value;

        public InvalidApplicationUserSurnameException(string value) : base($"ApplicationUser Surname: {value} does not only contain letters")
        {
            Value = value;
        }
    }
}