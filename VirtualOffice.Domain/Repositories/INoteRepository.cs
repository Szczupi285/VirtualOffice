using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.Note;

namespace VirtualOffice.Domain.Repositories
{
    public interface INoteRepository
    {
        Task<Note> GetById(NoteId guid);

        Task AddAsync(Note note);

        Task UpdateAsync(Note note);

        Task DeleteAsync(NoteId guid);
    }
}