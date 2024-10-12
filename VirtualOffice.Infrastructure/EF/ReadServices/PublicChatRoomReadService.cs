using Microsoft.EntityFrameworkCore;
using VirtualOffice.Application.Services;

namespace VirtualOffice.Infrastructure.EF.ReadServices
{
    public class PublicChatRoomReadService : IPublicChatRoomReadService
    {
        private readonly WriteDbContext _dbContext;

        public PublicChatRoomReadService(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.EmployeeTasks.AnyAsync(e => e.Id.Value == id);
        }
    }
}