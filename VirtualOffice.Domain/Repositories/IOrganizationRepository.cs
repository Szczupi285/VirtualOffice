using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.Organization;

namespace VirtualOffice.Domain.Repositories
{
    public interface IOrganizationRepository
    {
        Task<Organization> GetByIdAsync(OrganizationId guid, CancellationToken cancellationToken = default);

        Task AddAsync(Organization organization, CancellationToken cancellationToken = default);

        Task UpdateAsync(Organization organization, CancellationToken cancellationToken = default);

        Task DeleteAsync(Organization organization, CancellationToken cancellationToken = default);
    }
}