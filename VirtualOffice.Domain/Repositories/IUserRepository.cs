using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Domain.Repositories
{
    public interface IUserRepository
    {
        ApplicationUser GetById(Guid id);
        void Add(ApplicationUser user);
        void Update(ApplicationUser user);
        void Delete(Guid id);
    }
}
