using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.AbstractChatRoom;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace VirtualOffice.Domain.Repositories
{
    public interface IPrivateChatRoomRepository
    {
        Task<PrivateChatRoom> GetById(ChatRoomId guid);

        Task AddAsync(PrivateChatRoom chatRoom);

        Task UpdateAsync(PrivateChatRoom chatRoom);

        Task DeleteAsync(ChatRoomId id);
    }
}