using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.Note;

namespace VirtualOffice.Domain.Repositories
{
    public interface INoteRepository
    {
        Task<Note> GetById(NoteId guid);

        Task Add(Note note);

        Task Update(Note note);

        Task Delete(NoteId guid);

        Task<IEnumerable<Note>> GetAllForUser(ApplicationUser userId);

        Task<IEnumerable<Note>> GetAllSortedByTitleForUser(ApplicationUser userId);

        Task SaveAsync(CancellationToken cancellationToken);
    }
}