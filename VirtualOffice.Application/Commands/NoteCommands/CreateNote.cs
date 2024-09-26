using MediatR;

namespace VirtualOffice.Application.Commands.NoteCommands
{
    public record CreateNote(string Title, string Content, Guid UserId) : IRequest;
}
