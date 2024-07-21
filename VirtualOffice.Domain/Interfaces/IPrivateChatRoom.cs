using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace VirtualOffice.Domain.Interfaces
{
    public interface IPrivateChatRoom
    {
        SortedSet<Message> _Messages { get; }
        HashSet<ApplicationUser> _Participants { get; }

        ApplicationUser GetParticipantById(ApplicationUserId id);
        void SendMessage(ApplicationUser sender, string content);
    }
}