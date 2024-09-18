using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Document
{
    public class TooLongDocumentFilePathException : VirtualOfficeException
    {
        private string Value;

        public TooLongDocumentFilePathException(string value, uint length) : base($"File Path: {value} is more than {length} characters long")
        {
            Value = value;
        }
    }
}