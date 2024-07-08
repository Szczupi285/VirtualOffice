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
        Organization GetById(OrganizationId guid);
        void Add(Organization organization);
        void Update(Organization organization);
        void Delete(OrganizationId guid);
        IEnumerable<Office> GetOffices(OrganizationId organizationId);
        IEnumerable<ApplicationUser> GetUsers(OrganizationId organizationId);
        IEnumerable<Subscription> GetSubscriptions(OrganizationId organizationId);
        Task SaveAsync(CancellationToken cancellationToken);
    }
}
