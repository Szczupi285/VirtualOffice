using Microsoft.EntityFrameworkCore;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.NoteService;
using VirtualOffice.Domain.Repositories;
using VirtualOffice.Domain.ValueObjects.Note;

namespace VirtualOffice.Infrastructure.EF.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly WriteDbContext _dbContext;

        public NoteRepository(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Note> GetByIdAsync(NoteId guid, CancellationToken cancellationToken = default)
            => await _dbContext.Notes
            .FirstOrDefaultAsync(n => n.Id == guid, cancellationToken) ?? throw new NoteNotFoundException(guid);

        public async Task AddAsync(Note note, CancellationToken cancellationToken = default)
        {
            await _dbContext.Notes.AddAsync(note, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Note note, CancellationToken cancellationToken = default)
        {
            _dbContext.Notes.Remove(note);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Note note, CancellationToken cancellationToken = default)
        {
            _dbContext.Notes.Update(note);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}