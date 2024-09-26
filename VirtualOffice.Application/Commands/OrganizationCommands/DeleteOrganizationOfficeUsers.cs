using MediatR;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Application.Commands.OrganizationCommands
{
    public record DeleteOrganizationOfficeUsers(Guid OrganizationId, Guid OfficeId, ICollection<ApplicationUser> Users) : IRequest;

}
