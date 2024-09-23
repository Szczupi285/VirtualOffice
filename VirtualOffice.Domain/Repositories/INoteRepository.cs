using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.Note;

namespace VirtualOffice.Domain.Repositories
{
    public interface INoteRepository
    {
        Task<Note> GetByIdAsync(NoteId guid);

        Task<Note> GetByIdAsync(NoteId guid, CancellationToken cancellationToken);

        Task AddAsync(Note note);

        Task AddAsync(Note note, CancellationToken cancellationToken);

        Task UpdateAsync(Note note);

        Task UpdateAsync(Note note, CancellationToken cancellationToken);

        Task DeleteAsync(Note note);

        Task DeleteAsync(Note note, CancellationToken cancellationToken);
    }
}