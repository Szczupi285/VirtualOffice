using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.DTO.ApplicationUser;

namespace VirtualOffice.Application.Queries.Organization
{
    public record GetOrganizationUsers(Guid OrganizationId) : IRequest<IEnumerable<ApplicationUserDTO>>;
}
