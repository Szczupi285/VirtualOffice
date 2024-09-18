using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.AbstractChatRoom;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace VirtualOffice.Domain.Repositories
{
    public interface IPrivateChatRoomRepository
    {
        Task<PrivateChatRoom> GetById(ChatRoomId guid);

        Task Add(PrivateChatRoom chatRoom);

        Task Update(PrivateChatRoom chatRoom);

        Task Delete(ChatRoomId id);

        Task<IEnumerable<PrivateChatRoom>> GetAllByUserId(ApplicationUserId id);

        Task SaveAsync(CancellationToken cancellationToken);
    }
}