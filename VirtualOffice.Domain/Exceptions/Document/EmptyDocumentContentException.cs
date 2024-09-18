using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Document
{
    public class EmptyDocumentContentException : VirtualOfficeException
    {
        public EmptyDocumentContentException() : base("Document content cannot be empty")
        {
        }
    }
}