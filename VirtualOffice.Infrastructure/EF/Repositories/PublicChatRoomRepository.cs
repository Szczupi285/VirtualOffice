using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.Repositories;
using VirtualOffice.Domain.Repositories;
using VirtualOffice.Domain.ValueObjects.AbstractChatRoom;

namespace VirtualOffice.Infrastructure.EF.Repositories
{
    public class PublicChatRoomRepository : IPublicChatRoomRepository
    {
        private readonly WriteDbContext _dbContext;

        public PublicChatRoomRepository(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PublicChatRoom> GetByIdAsync(ChatRoomId guid)
            => await _dbContext.PublicChatRooms
            .Include(pcr => pcr._Participants)
            .Include(pcr => pcr._Messages)
            .FirstOrDefaultAsync(pcr => pcr.Id == guid) ?? throw new PublicChatRoomNotFoundException(guid);

        public async Task<PublicChatRoom> GetByIdAsync(ChatRoomId guid, CancellationToken cancellationToken)
         => await _dbContext.PublicChatRooms
            .Include(pcr => pcr._Participants)
            .Include(pcr => pcr._Messages)
            .FirstOrDefaultAsync(pcr => pcr.Id == guid, cancellationToken) ?? throw new PublicChatRoomNotFoundException(guid);

        public async Task AddAsync(PublicChatRoom chatRoom)
        {
            await _dbContext.PublicChatRooms.AddAsync(chatRoom);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddAsync(PublicChatRoom chatRoom, CancellationToken cancellationToken)
        {
            await _dbContext.PublicChatRooms.AddAsync(chatRoom, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(PublicChatRoom PublicChatRoom)
        {
            _dbContext.PublicChatRooms.Remove(PublicChatRoom);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(PublicChatRoom PublicChatRoom, CancellationToken cancellationToken)
        {
            _dbContext.PublicChatRooms.Remove(PublicChatRoom);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(PublicChatRoom chatRoom)
        {
            _dbContext.PublicChatRooms.Update(chatRoom);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(PublicChatRoom chatRoom, CancellationToken cancellationToken)
        {
            _dbContext.PublicChatRooms.Update(chatRoom);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}