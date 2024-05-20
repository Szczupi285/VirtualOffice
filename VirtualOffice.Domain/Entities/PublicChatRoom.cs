using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Exceptions.ChatRoom;
using VIrtualOffice.Domain.Exceptions.ScheduleItem;
using VirtualOffice.Domain.ValueObjects.AbstractChatRoom;
using VirtualOffice.Domain.ValueObjects.ChatRoom;
using VirtualOffice.Domain.DomainEvents.PublicChatRoomEvents;

namespace VirtualOffice.Domain.Entities
{
    public class PublicChatRoom : AbstractChatRoom
    {

        public PublicChatRoomName _Name { get; private set; }
        public PublicChatRoom(ChatRoomId id, HashSet<ApplicationUser> participants, SortedSet<Message> messages, PublicChatRoomName name) : base(id, participants, messages)
        {
            _Name = name;
        }

        // we don't check for duplicates since it's a hashSet
        public void AddParticipant(ApplicationUser participant)
        {
            if (participant is null)
                throw new ArgumentNullException(nameof(participant));
            _Participants.Add(participant);
            AddEvent(new ChatRoomParticipantAdded(this, participant));
        }
        public void AddRangeParticipants(ICollection<ApplicationUser> participants)
        {
            foreach(var participant in participants)
            {
                AddParticipant(participant);
            }
        }
        public void RemoveParticipant(ApplicationUser participant)
        {
            if(participant is null)
                throw new ArgumentNullException(nameof(participant));
            else if (!_Participants.Contains(participant))
                throw new UserIsNotAParticipantOfThisChatException(participant.Id);
            // if last person want to leave Public Chat Room, then room must be deleted
            else if (_Participants.Count == 1)
                throw new ChatRoomCannotBeEmptyException();

            _Participants.Remove(participant);
            AddEvent(new ChatRoomParticipantRemoved(this, participant));
        }
        public void RemoveRangeParticipants(ICollection<ApplicationUser> participants)
        {
            foreach (var participant in participants)
            {
                RemoveParticipant(participant);
            }
        }

        public void SetName(string name)
        {
            _Name = name;
            AddEvent(new ChatRoomNameSetted(this, _Name));
        }
        
    }
}
