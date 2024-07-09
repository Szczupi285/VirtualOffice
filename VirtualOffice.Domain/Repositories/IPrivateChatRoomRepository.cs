using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.AbstractChatRoom;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

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
