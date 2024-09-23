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

        public async Task<Note> GetByIdAsync(NoteId guid)
            => await _dbContext.Notes
            .FirstOrDefaultAsync(n => n.Id == guid) ?? throw new NoteNotFoundException(guid);

        public async Task<Note> GetByIdAsync(NoteId guid, CancellationToken cancellationToken)
            => await _dbContext.Notes
            .FirstOrDefaultAsync(n => n.Id == guid, cancellationToken) ?? throw new NoteNotFoundException(guid);

        public async Task AddAsync(Note note)
        {
            await _dbContext.Notes.AddAsync(note);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddAsync(Note note, CancellationToken cancellationToken)
        {
            await _dbContext.Notes.AddAsync(note, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Note note)
        {
            _dbContext.Notes.Remove(note);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Note note, CancellationToken cancellationToken)
        {
            _dbContext.Notes.Remove(note);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(Note note)
        {
            _dbContext.Notes.Update(note);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Note note, CancellationToken cancellationToken)
        {
            _dbContext.Notes.Update(note);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}