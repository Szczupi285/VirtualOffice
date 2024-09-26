using MediatR;

namespace VirtualOffice.Application.Commands.NoteCommands
{
    public record DeleteNote(Guid Id) : IRequest;
}
