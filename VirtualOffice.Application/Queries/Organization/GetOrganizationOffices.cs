using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.DTO.Office;

namespace VirtualOffice.Application.Queries.Organization
{
    public record GetOrganizationOffices(Guid OrganizationId) : IRequest<IEnumerable<OfficeIdAndNameDTO>>;
}
