using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.AbstractChatRoom;

namespace VirtualOffice.Domain.Repositories
{
    public interface IPrivateChatRoomRepository
    {
        Task<PrivateChatRoom> GetByIdAsync(ChatRoomId guid);

        Task<PrivateChatRoom> GetByIdAsync(ChatRoomId guid, CancellationToken cancellationToken);

        Task AddAsync(PrivateChatRoom chatRoom);

        Task AddAsync(PrivateChatRoom chatRoom, CancellationToken cancellationToken);

        Task UpdateAsync(PrivateChatRoom chatRoom);

        Task UpdateAsync(PrivateChatRoom chatRoom, CancellationToken cancellationToken);

        Task DeleteAsync(PrivateChatRoom chatRoom);

        Task DeleteAsync(PrivateChatRoom chatRoom, CancellationToken cancellationToken);
    }
}