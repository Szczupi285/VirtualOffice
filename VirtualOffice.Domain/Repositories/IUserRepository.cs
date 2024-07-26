using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace VirtualOffice.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<ApplicationUser> GetById(ApplicationUserId id);
        Task Add(ApplicationUser user);
        Task Update(ApplicationUser user);
        Task Delete(ApplicationUserId id);
        Task SaveAsync(CancellationToken cancellationToken);
    }
}
