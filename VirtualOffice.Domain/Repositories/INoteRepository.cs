using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Domain.Repositories
{
    public interface INoteRepository
    {
        Note GetById(Guid guid);
        void Add(Note user);
        void Update(Note user);
        void Delete(Guid id);
        IEnumerable<Note> GetAllForUser(Guid userId);
        IEnumerable<Note> GetAllSortedForUser(Guid userId);
    }
}
