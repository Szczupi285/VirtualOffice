using MediatR;

namespace VirtualOffice.Application.Commands.UserCommands
{
    public record DeleteUser(Guid Id) : IRequest
    {
    }
}
