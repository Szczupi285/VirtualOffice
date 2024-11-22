using VirtualOffice.Domain.Entities;

namespace VirtualOffice.Application.DTO.PrivateChatRoom
{
    public class CreatePrivateChatRoomRequest
    {
        public HashSet<Domain.Entities.ApplicationUser> Participants { get; set; }
        public SortedSet<Message> Messages { get; set; }
    }
}