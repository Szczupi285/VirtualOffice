﻿using System;
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
        public PublicChatRoom(ChatRoomId id, HashSet<ApplicationUser> participants, SortedSet<Message> messages, PublicChatRoomName name) : base(id, participants, messages)
        {
            _Name = name;
        }
        // we don't check for duplicates since it's a hashSet
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
            if (!_Participants.Contains(participant))
                throw new UserIsNotAParticipantOfThisChat(participant.Id);
            else if (_Participants.Count == 1)
                throw new ChatRoomCannotHaveNoParticipants();

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
