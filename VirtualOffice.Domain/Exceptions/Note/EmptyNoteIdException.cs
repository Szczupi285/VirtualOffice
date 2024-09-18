using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Note
{
    public class EmptyNoteIdException : VirtualOfficeException
    {
        public EmptyNoteIdException()
            : base("Note Id cannot be empty")
        {
        }
    }
}