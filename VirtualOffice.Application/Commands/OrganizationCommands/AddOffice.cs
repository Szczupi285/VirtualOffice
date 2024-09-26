using MediatR;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Application.Commands.OrganizationCommands
{
    public record AddOffice(Guid OrganizationId, string Name, string Description, HashSet<ApplicationUser> Members) : IRequest;
}
