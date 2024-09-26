using MediatR;
using VirtualOffice.Application.DTO.ApplicationUser;

namespace VirtualOffice.Application.Queries.Organization
{
    public record GetOrganizationUsers(Guid OrganizationId) : IRequest<IEnumerable<ApplicationUserDTO>>;
}
