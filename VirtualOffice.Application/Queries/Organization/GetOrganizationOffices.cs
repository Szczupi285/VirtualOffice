using MediatR;
using VirtualOffice.Application.DTO.Office;

namespace VirtualOffice.Application.Queries.Organization
{
    public record GetOrganizationOffices(Guid OrganizationId) : IRequest<IEnumerable<OfficeIdAndNameDTO>>;
}
