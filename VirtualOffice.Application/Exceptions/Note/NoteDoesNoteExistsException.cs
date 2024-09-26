using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Application.Exceptions.Note
{
    public class NoteDoesNoteExistsException : VirtualOfficeException
    {
        public NoteDoesNoteExistsException(Guid id) : base($"Note with Id: {id} does not exist")
        {
        }
    }
}
