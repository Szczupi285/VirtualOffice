using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Document
{
    public class TooLongDocumentTitleException : VirtualOfficeException
    {
        private string Value;

        public TooLongDocumentTitleException(string value) : base($"Title: {value} is more than 50 characters long")
        {
            Value = value;
        }
    }
}