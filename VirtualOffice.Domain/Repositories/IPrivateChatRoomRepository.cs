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
        PrivateChatRoom GetById(ChatRoomId guid);
        void Add(PrivateChatRoom chatRoom);
        void Update(PrivateChatRoom chatRoom);
        void Delete(ChatRoomId id);
        IEnumerable<PrivateChatRoom> GetAllByUserId(ApplicationUserId id);
    }
}
