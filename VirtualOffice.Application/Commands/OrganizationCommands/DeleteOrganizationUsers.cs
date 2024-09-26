using MediatR;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Application.Commands.OrganizationCommands
{
    public record DeleteOrganizationUsers(Guid Id, ICollection<ApplicationUser> Users) : IRequest;
}
