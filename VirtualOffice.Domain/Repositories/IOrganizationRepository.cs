using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.Organization;

namespace VirtualOffice.Domain.Repositories
{
    public interface IOrganizationRepository
    {
        Task<Organization> GetById(OrganizationId guid);
        Task Add(Organization organization);
        Task Update(Organization organization);
        Task Delete(OrganizationId guid);
        Task<IEnumerable<Office>> GetOffices(OrganizationId organizationId);
        Task<IEnumerable<ApplicationUser>> GetUsers(OrganizationId organizationId);
        Task<IEnumerable<Subscription>> GetSubscriptions(OrganizationId organizationId);
        Task SaveAsync(CancellationToken cancellationToken);
    }
}
