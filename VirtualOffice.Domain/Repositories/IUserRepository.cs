using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace VirtualOffice.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetById(ApplicationUserId id);

        Task AddAsync(ApplicationUser user);

        Task UpdateAsync(ApplicationUser user);

        Task DeleteAsync(ApplicationUserId id);
    }
}