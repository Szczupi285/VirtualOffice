using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.AbstractChatRoom;

namespace VirtualOffice.Domain.Repositories
{
    public interface IPrivateChatRoomRepository
    {
        Task<PrivateChatRoom> GetByIdAsync(ChatRoomId guid, CancellationToken cancellationToken = default);

        Task AddAsync(PrivateChatRoom chatRoom, CancellationToken cancellationToken = default);

        Task UpdateAsync(PrivateChatRoom chatRoom, CancellationToken cancellationToken = default);

        Task DeleteAsync(PrivateChatRoom chatRoom, CancellationToken cancellationToken = default);
    }
}