using MediatR;

namespace VirtualOffice.Application.Commands.UserCommands
{
    public record UpdateUser(Guid Id, string Name, string Surname) : IRequest;

}
