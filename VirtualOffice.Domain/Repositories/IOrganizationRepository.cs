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
        void Add(Organization user);
        void Update(Organization user);
        void Delete(OrganizationId id);
        IEnumerable<Office> GetOffices(OrganizationId organizationId);
        IEnumerable<ApplicationUser> GetUsers(OrganizationId organizationId);
        IEnumerable<Subscription> GetSubscriptions(OrganizationId organizationId);
    }
}
