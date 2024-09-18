using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Document
{
    public class InvalidDocumentFilePathException : VirtualOfficeException
    {
        private string Value;

        public InvalidDocumentFilePathException(string value) : base($"File path : {value} is not a valid windows server File Path")
        {
            Value = value;
        }
    }
}