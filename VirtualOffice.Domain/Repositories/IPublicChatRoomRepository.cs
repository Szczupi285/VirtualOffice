using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.AbstractChatRoom;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace VirtualOffice.Domain.Repositories
{
    public interface IPublicChatRoomRepository
    {
        Task<PublicChatRoom> GetById(ChatRoomId guid);

        Task AddAsync(PublicChatRoom chatRoom);

        Task UpdateAsync(PublicChatRoom chatRoom);

        Task DeleteAsync(ChatRoomId id);
    }
}