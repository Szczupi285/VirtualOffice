using MediatR;
using VirtualOffice.Domain.Consts;

namespace VirtualOffice.Application.Commands.UserCommands
{
    public record CreateUser(string Name, string Surname, PermissionsEnum Permissions) : IRequest;
}
