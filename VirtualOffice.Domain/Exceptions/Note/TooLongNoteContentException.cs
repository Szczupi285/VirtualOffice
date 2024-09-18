using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Note
{
    public class TooLongNoteContentException : VirtualOfficeException
    {
        private string Value;

        public TooLongNoteContentException(string value, ushort length) : base($"Note Content: {value} is more than {length} characters long")
        {
            Value = value;
        }
    }
}