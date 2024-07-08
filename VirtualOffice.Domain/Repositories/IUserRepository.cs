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
        ApplicationUser GetById(ApplicationUserId id);
        void Add(ApplicationUser user);
        void Update(ApplicationUser user);
        void Delete(ApplicationUserId id);
        Task SaveAsync(CancellationToken cancellationToken);
    }
}
