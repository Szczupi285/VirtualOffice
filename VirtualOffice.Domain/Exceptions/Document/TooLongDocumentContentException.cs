using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Document
{
    public class TooLongDocumentContentException : VirtualOfficeException
    {
        private string Value;

        public TooLongDocumentContentException(string value, uint length) : base($"Document Content: {value} is more than {length} characters long")
        {
            Value = value;
        }
    }
}