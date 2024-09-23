using Microsoft.EntityFrameworkCore;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.Repositories;
using VirtualOffice.Domain.Repositories;
using VirtualOffice.Domain.ValueObjects.AbstractChatRoom;

namespace VirtualOffice.Infrastructure.EF.Repositories
{
    public class PrivateChatRoomRepository : IPrivateChatRoomRepository
    {
        private readonly WriteDbContext _dbContext;

        public PrivateChatRoomRepository(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PrivateChatRoom> GetByIdAsync(ChatRoomId guid)
            => await _dbContext.PrivateChatRooms
            .Include(pcr => pcr._Participants)
            .Include(pcr => pcr._Messages)
            .FirstOrDefaultAsync(pcr => pcr.Id == guid) ?? throw new PrivateChatRoomNotFoundException(guid);

        public async Task<PrivateChatRoom> GetByIdAsync(ChatRoomId guid, CancellationToken cancellationToken)
            => await _dbContext.PrivateChatRooms
            .Include(pcr => pcr._Participants)
            .Include(pcr => pcr._Messages)
            .FirstOrDefaultAsync(pcr => pcr.Id == guid, cancellationToken) ?? throw new PrivateChatRoomNotFoundException(guid);

        public async Task AddAsync(PrivateChatRoom chatRoom)
        {
            await _dbContext.PrivateChatRooms.AddAsync(chatRoom);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddAsync(PrivateChatRoom chatRoom, CancellationToken cancellationToken)
        {
            await _dbContext.PrivateChatRooms.AddAsync(chatRoom, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(PrivateChatRoom chatRoom)
        {
            _dbContext.PrivateChatRooms.Remove(chatRoom);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(PrivateChatRoom chatRoom, CancellationToken cancellationToken)
        {
            _dbContext.PrivateChatRooms.Remove(chatRoom);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(PrivateChatRoom chatRoom)
        {
            _dbContext.PrivateChatRooms.Update(chatRoom);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(PrivateChatRoom chatRoom, CancellationToken cancellationToken)
        {
            _dbContext.PrivateChatRooms.Update(chatRoom);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}