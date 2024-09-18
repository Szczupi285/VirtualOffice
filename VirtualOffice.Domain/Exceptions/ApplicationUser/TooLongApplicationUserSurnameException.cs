using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.ApplicationUser
{
    public class TooLongApplicationUserSurnameException : VirtualOfficeException
    {
        private string Value;

        public TooLongApplicationUserSurnameException(string value)
            : base($"User surname: {value} is more than 35 characters long")
        {
            Value = value;
        }
    }
}