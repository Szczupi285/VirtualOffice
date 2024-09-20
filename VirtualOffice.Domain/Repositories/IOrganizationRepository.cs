using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.Organization;

namespace VirtualOffice.Domain.Repositories
{
    public interface IOrganizationRepository
    {
        Task<Organization> GetById(OrganizationId guid);

        Task AddAsync(Organization organization);

        Task UpdateAsync(Organization organization);

        Task DeleteAsync(OrganizationId guid);
    }
}