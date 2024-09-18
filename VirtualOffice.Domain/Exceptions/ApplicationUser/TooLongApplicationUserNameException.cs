using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.ApplicationUser
{
    public class TooLongApplicationUserNameException : VirtualOfficeException
    {
        private string Value;

        public TooLongApplicationUserNameException(string value)
            : base($"User Name: {value} is more than 30 characters long")
        {
            Value = value;
        }
    }
}