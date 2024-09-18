using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.Note
{
    public class EmptyNoteTitleException : VirtualOfficeException
    {
        public EmptyNoteTitleException() : base("Note title cannot be empty")
        {
        }
    }
}