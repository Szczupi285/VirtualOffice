﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.ChatRoomService;
using VirtualOffice.Domain.Services.ChatRoom;
namespace DomainUnitTests
{
    public class PrivateChatRoomServiceTests
    {
        private HashSet<PrivateChatRoom> _chatRooms;
        private PrivateChatRoom _chatRoom;
        private PrivateChatRoomService _chatRoomService;
        private HashSet<ApplicationUser> _Participants;
        private SortedSet<Message> _Messages = new SortedSet<Message>();
        private ApplicationUser user = new ApplicationUser(Guid.NewGuid(), "Name", "Surname");
        private ApplicationUser user1 = new ApplicationUser(Guid.NewGuid(), "Name", "Surname");
        private ApplicationUser UserNotAdded = new ApplicationUser(Guid.NewGuid(), "Name", "Surname");
        private Guid _chatRoomId = Guid.NewGuid();
        public PrivateChatRoomServiceTests()
        {
            HashSet<ApplicationUser> participants = new HashSet<ApplicationUser>();
            participants.Add(user);
            participants.Add(user1);
            _Participants = participants;
            _chatRoom = new PrivateChatRoom(_chatRoomId, _Participants, _Messages);
            _chatRooms = new HashSet<PrivateChatRoom>();
            _chatRoomService = new PrivateChatRoomService(_chatRooms);
        }

        [Fact]
        public void AddChatRoom_ShouldAddChatRoom()
        {
            _chatRoomService.AddChatRoom(_chatRoom);
            Assert.Contains(_chatRoom, _chatRooms);
        }

        [Fact]
        public void DeleteChatRoom_ShouldRemoveChatRoom()
        {
            _chatRooms.Add(_chatRoom);
            Assert.Contains(_chatRoom, _chatRooms);
            _chatRoomService.DeleteChatRoom(_chatRoomId);
            Assert.DoesNotContain(_chatRoom, _chatRooms);
        }

        [Fact]
        public void DeleteChatRoom_NonExistentId_ShouldThrowException()
        {
            Assert.Throws<ChatRoomIdNotFoundException>(() => _chatRoomService.DeleteChatRoom(Guid.NewGuid()));
        }

        [Fact]
        public void GetChatRoom_ShouldReturnChatRoom()
        {
            _chatRooms.Add(_chatRoom);
            var result = _chatRoomService.GetChatRoom(_chatRoomId);
            Assert.Equal(_chatRoom, result);
        }

        [Fact]
        public void GetChatRoom_NonExistentId_ShouldThrowException()
        {
            Assert.Throws<ChatRoomIdNotFoundException>(() => _chatRoomService.GetChatRoom(Guid.NewGuid()));
        }

        [Fact]
        public void GetChatRoomsForUser_ShouldReturnUserChatRooms()
        {
            PrivateChatRoom testChat = new PrivateChatRoom(Guid.NewGuid(), _Participants, _Messages);
            _chatRoomService.AddChatRoom(_chatRoom);
            _chatRoomService.AddChatRoom(testChat);
            var result = _chatRoomService.GetChatRoomsForUser(user);
            Assert.Contains(_chatRoom, result);
            Assert.Contains(testChat, result);
        }
        [Fact]
        public void GetChatRoomsForUser_ShouldReturnEmptyList()
        {
            PrivateChatRoom testChat = new PrivateChatRoom(Guid.NewGuid(), _Participants, _Messages);
            _chatRooms.Add(testChat);
            var result = _chatRoomService.GetChatRoomsForUser(UserNotAdded);
            Assert.Empty(result);

        }
    }
}


