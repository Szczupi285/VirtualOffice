using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.Organization;

namespace VirtualOffice.Domain.Repositories
{
    public interface IOrganizationRepository
    {
        Task<Organization> GetByIdAsync(OrganizationId guid);

        Task<Organization> GetByIdAsync(OrganizationId guid, CancellationToken cancellationToken);

        Task AddAsync(Organization organization);

        Task AddAsync(Organization organization, CancellationToken cancellationToken);

        Task UpdateAsync(Organization organization);

        Task UpdateAsync(Organization organization, CancellationToken cancellationToken);

        Task DeleteAsync(Organization organization);

        Task DeleteAsync(Organization organization, CancellationToken cancellationToken);
    }
}