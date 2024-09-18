using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Office
{
    public class InvalidOfficeDescriptionException : VirtualOfficeException
    {
        private string Value;

        public InvalidOfficeDescriptionException(string value) : base($"Description: {value} is more than 200 characters long")
        {
            Value = value;
        }
    }
}