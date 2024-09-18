using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.AbstractChatRoom;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace VirtualOffice.Domain.Repositories
{
    public interface IPublicChatRoomRepository
    {
        Task<PublicChatRoom> GetById(ChatRoomId guid);

        Task Add(PublicChatRoom chatRoom);

        Task Update(PublicChatRoom chatRoom);

        Task Delete(ChatRoomId id);

        Task<IEnumerable<PublicChatRoom>> GetAllByUserId(ApplicationUserId id);

        Task SaveAsync(CancellationToken cancellationToken);
    }
}