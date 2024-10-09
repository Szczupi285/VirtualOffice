using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace VirtualOffice.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetByIdAsync(ApplicationUserId guid, CancellationToken cancellationToken = default);

        Task AddAsync(ApplicationUser user, CancellationToken cancellationToken = default);

        Task UpdateAsync(ApplicationUser user, CancellationToken cancellationToken = default);

        Task DeleteAsync(ApplicationUser user, CancellationToken cancellationToken = default);
    }
}