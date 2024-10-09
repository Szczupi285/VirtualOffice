using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.Note;

namespace VirtualOffice.Domain.Repositories
{
    public interface INoteRepository
    {
        Task<Note> GetByIdAsync(NoteId guid, CancellationToken cancellationToken = default);

        Task AddAsync(Note note, CancellationToken cancellationToken = default);

        Task UpdateAsync(Note note, CancellationToken cancellationToken = default);

        Task DeleteAsync(Note note, CancellationToken cancellationToken = default);
    }
}