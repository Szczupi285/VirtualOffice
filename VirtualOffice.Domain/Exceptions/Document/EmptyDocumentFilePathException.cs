using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Document
{
    public class EmptyDocumentFilePathException : VirtualOfficeException
    {
        private string Value;

        public EmptyDocumentFilePathException(string value) : base($"DocumentFilePath: '{value}' cannot be empty")
        {
            Value = value;
        }
    }
}