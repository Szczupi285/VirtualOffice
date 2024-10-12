using Microsoft.EntityFrameworkCore;
using VirtualOffice.Application.Services;

namespace VirtualOffice.Infrastructure.EF.ReadServices
{
    public class PrivateChatRoomReadService : IPrivateChatRoomReadService
    {
        private readonly WriteDbContext _dbContext;

        public PrivateChatRoomReadService(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.PrivateChatRooms.AnyAsync(e => e.Id.Value == id);
        }
    }
}