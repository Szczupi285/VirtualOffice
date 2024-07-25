using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Consts;

namespace VirtualOffice.Application.Commands.UserCommands
{
    public record CreateUser(string Name, string Surname, PermissionsEnum Permissions) : IRequest;
}
