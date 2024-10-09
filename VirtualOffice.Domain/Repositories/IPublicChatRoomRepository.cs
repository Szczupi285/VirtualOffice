using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.AbstractChatRoom;

namespace VirtualOffice.Domain.Repositories
{
    public interface IPublicChatRoomRepository
    {
        Task<PublicChatRoom> GetByIdAsync(ChatRoomId guid, CancellationToken cancellationToken = default);

        Task AddAsync(PublicChatRoom chatRoom, CancellationToken cancellationToken = default);

        Task UpdateAsync(PublicChatRoom chatRoom, CancellationToken cancellationToken = default);

        Task DeleteAsync(PublicChatRoom chatRoom, CancellationToken cancellationToken = default);
    }
}