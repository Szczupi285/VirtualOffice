using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.Note;

namespace VirtualOffice.Domain.Repositories
{
    public interface INoteRepository
    {
        Note GetById(NoteId guid);
        void Add(Note user);
        void Update(Note user);
        void Delete(NoteId id);
        IEnumerable<Note> GetAllForUser(ApplicationUser userId);
        IEnumerable<Note> GetAllSortedForUser(ApplicationUser userId);
    }
}
