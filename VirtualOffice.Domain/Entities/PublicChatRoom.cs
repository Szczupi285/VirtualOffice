using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Exceptions.ChatRoom;
using VirtualOffice.Domain.Exceptions.EmployeeTask;
using VirtualOffice.Domain.ValueObjects.AbstractChatRoom;
using VirtualOffice.Domain.ValueObjects.ChatRoom;

namespace VirtualOffice.Domain.Entities
{
    public class PublicChatRoom : AbstractChatRoom
    {

        public PublicChatRoomName _Name {get; private set; }
        public PublicChatRoom(ChatRoomId id, HashSet<ApplicationUser> participants, SortedSet<Message> messages) : base(id, participants, messages)
        {
        }
        public void AddParticipant(ApplicationUser participant)
        {
            _Participants.Add(participant);
        }
        public void AddRangeParticipant(ICollection<ApplicationUser> participants)
        {
            foreach(var participant in _Participants)
            {
                AddParticipant(participant);
            }
        }
        public void RemoveParticipant(ApplicationUser participant)
        {
            _Participants.Remove(participant);
        }
        public void RemoveRangeParticipant(ICollection<ApplicationUser> participants)
        {
            foreach (var participant in _Participants)
            {
                RemoveParticipant(participant);
            }
        }

        public void SetName(string name) => _Name = name;

        
    }
}
