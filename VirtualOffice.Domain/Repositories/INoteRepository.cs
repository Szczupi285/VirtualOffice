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
        void Add(Note note);
        void Update(Note note);
        void Delete(NoteId guid);
        IEnumerable<Note> GetAllForUser(ApplicationUser userId);
        IEnumerable<Note> GetAllSortedByTitleForUser(ApplicationUser userId);
    }
}
