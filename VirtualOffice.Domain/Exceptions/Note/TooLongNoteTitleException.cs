using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Note
{
    public class TooLongNoteTitleException : VirtualOfficeException
    {
        private string Value;

        public TooLongNoteTitleException(string value, ushort length) : base($"Note Title: {value} is more than {length} characters long")
        {
            Value = value;
        }
    }
}