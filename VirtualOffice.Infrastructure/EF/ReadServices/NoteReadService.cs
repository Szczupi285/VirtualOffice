using Microsoft.EntityFrameworkCore;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.ValueObjects.Note;

namespace VirtualOffice.Infrastructure.EF.ReadServices
{
    public class NoteReadService : INoteReadService
    {
        private readonly WriteDbContext _dbContext;

        public NoteReadService(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Notes.AnyAsync(e => e.Id == new NoteId(id));
        }
    }
}