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

        public PublicChatRoomName _Name { get; private set; }
        public PublicChatRoom(ChatRoomId id, HashSet<ApplicationUser> participants, SortedSet<Message> messages, PublicChatRoomName name) : base(id, participants, messages)
        {
            _Name = name;
        }

        // we don't check for duplicates since it's a hashSet
        public void AddParticipant(ApplicationUser participant)
        {
            _Participants.Add(participant);
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
            if (!_Participants.Contains(participant))
                throw new UserIsNotAParticipantOfThisChat(participant.Id);
            // if last person want to leave Public Chat Room, then room must be deleted
            else if (_Participants.Count == 1)
                throw new ChatRoomCannotBeEmpty();

            _Participants.Remove(participant);
        }
        public void RemoveRangeParticipants(ICollection<ApplicationUser> participants)
        {
            foreach (var participant in participants)
            {
                RemoveParticipant(participant);
            }
        }

        public void SetName(string name) => _Name = name;

        
    }
}
