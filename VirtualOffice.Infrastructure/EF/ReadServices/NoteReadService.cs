using Microsoft.EntityFrameworkCore;
using VirtualOffice.Application.Services;

namespace VirtualOffice.Infrastructure.EF.ReadServices
{
    public class NoteReadService : INoteReadService
    {
        private readonly WriteDbContext _dbContext;

        public NoteReadService(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExistsByIdAsync(Guid id)
        {
            return await _dbContext.Notes.AnyAsync(e => e.Id.Value == id);
        }
    }
}