using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Document
{
    public class EmptyDocumentTitleException : VirtualOfficeException
    {
        public EmptyDocumentTitleException() : base("Document title cannot be empty")
        {
        }
    }
}