using VirtualOffice.Shared.Abstractions.Exceptions;

namespace VirtualOffice.Domain.Exceptions.NoteService
{
    public class NoteNotFoundException : VirtualOfficeException
    {
        private Guid Value;

        public NoteNotFoundException(Guid value) : base($"Note with Id: {value} has not been found")
        {
            Value = value;
        }
    }
}