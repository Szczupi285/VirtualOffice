using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace VirtualOffice.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetByIdAsync(ApplicationUserId guid);

        Task<ApplicationUser> GetByIdAsync(ApplicationUserId guid, CancellationToken cancellationToken);

        Task AddAsync(ApplicationUser user);

        Task AddAsync(ApplicationUser user, CancellationToken cancellationToken);

        Task UpdateAsync(ApplicationUser user);

        Task UpdateAsync(ApplicationUser user, CancellationToken cancellationToken);

        Task DeleteAsync(ApplicationUser user);

        Task DeleteAsync(ApplicationUser user, CancellationToken cancellationToken);
    }
}