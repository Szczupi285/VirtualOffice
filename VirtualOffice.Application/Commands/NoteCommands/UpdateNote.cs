using MediatR;

namespace VirtualOffice.Application.Commands.NoteCommands
{
    public record UpdateNote(Guid Id, string Title, string Content) : IRequest;
}
