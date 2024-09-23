using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.AbstractChatRoom;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace VirtualOffice.Domain.Repositories
{
    public interface IPublicChatRoomRepository
    {
        Task<PublicChatRoom> GetByIdAsync(ChatRoomId guid);

        Task<PublicChatRoom> GetByIdAsync(ChatRoomId guid, CancellationToken cancellationToken);

        Task AddAsync(PublicChatRoom chatRoom);

        Task AddAsync(PublicChatRoom chatRoom, CancellationToken cancellationToken);

        Task UpdateAsync(PublicChatRoom chatRoom);

        Task UpdateAsync(PublicChatRoom chatRoom, CancellationToken cancellationToken);

        Task DeleteAsync(PublicChatRoom chatRoom);

        Task DeleteAsync(PublicChatRoom chatRoom, CancellationToken cancellationToken);
    }
}