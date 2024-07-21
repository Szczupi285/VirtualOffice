using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.ChatRoom;

namespace VirtualOffice.Domain.Interfaces
{
    public interface IPublicChatRoom
    {
        PublicChatRoomName _Name { get; }
        SortedSet<Message> _Messages { get; }
        HashSet<ApplicationUser> _Participants { get; }

        ApplicationUser GetParticipantById(ApplicationUserId id);
        void SendMessage(ApplicationUser sender, string content);
        void AddParticipant(ApplicationUser participant);
        void AddRangeParticipants(ICollection<ApplicationUser> participants);
        void RemoveParticipant(ApplicationUser participant);
        void RemoveRangeParticipants(ICollection<ApplicationUser> participants);
        void SetName(string name);
    }
}