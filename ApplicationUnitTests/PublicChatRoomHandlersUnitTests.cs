﻿using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.Commands.Handlers.PublicChatRoomHandlers;
using VirtualOffice.Application.Commands.PublicChatRoomCommands;
using VirtualOffice.Application.Exceptions.PrivateChatRoom;
using VirtualOffice.Application.Exceptions.PublicChatRoom;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Repositories;

namespace ApplicationUnitTests
{
    public class PublicChatRoomHandlersUnitTests
    {
        private readonly Guid guid = Guid.NewGuid();
        private readonly Mock<IPublicChatRoomRepository> _repositoryMock;
        private readonly Mock<IPublicChatRoomReadService> _readServiceMock;
        private readonly Mock<IUserReadService> _userReadService;
        private readonly Mock<IUserRepository> _userRepository;
        private readonly AddChatParticipantsHandler _addChatPartHand;
        private readonly CreatePublicChatRoomHandler _crePubChatRoomHand;
        private readonly DeleteChatParticipantsHandler _delChatPartHand;
        private readonly DeletePublicChatRoomHandler _delPubChatRoomHand;
        private readonly SendPublicMessageHandler _sendPubMessHand;
        private readonly UpdatePublicChatRoomNameHandler _updPubChatRoomNameHand;
        private readonly PublicChatRoom _publicChatRoom;
        private readonly ApplicationUser _user1;
        private readonly ApplicationUser _user2;
        private readonly ApplicationUser _user3;

        public PublicChatRoomHandlersUnitTests()
        {
            _user1 = new ApplicationUser(Guid.NewGuid(), "John", "Doe");
            _user2 = new ApplicationUser(Guid.NewGuid(), "Jane", "Roe");
            _user3 = new ApplicationUser(Guid.NewGuid(), "Judy", "Poe");
            _repositoryMock = new Mock<IPublicChatRoomRepository>();
            _readServiceMock = new Mock<IPublicChatRoomReadService>();
            _userRepository = new Mock<IUserRepository>();
            _userReadService = new Mock<IUserReadService>();
            _addChatPartHand = new AddChatParticipantsHandler(_repositoryMock.Object, _readServiceMock.Object);
            _crePubChatRoomHand = new CreatePublicChatRoomHandler(_repositoryMock.Object);
            _delChatPartHand = new DeleteChatParticipantsHandler(_repositoryMock.Object, _readServiceMock.Object);
            _delPubChatRoomHand = new DeletePublicChatRoomHandler(_repositoryMock.Object, _readServiceMock.Object);
            _sendPubMessHand = new SendPublicMessageHandler(_repositoryMock.Object, _readServiceMock.Object,
                _userReadService.Object,  _userRepository.Object);
            _updPubChatRoomNameHand = new UpdatePublicChatRoomNameHandler(_repositoryMock.Object, _readServiceMock.Object);
            _publicChatRoom = new PublicChatRoom(guid, new HashSet<ApplicationUser>() {_user1, _user2 }, new SortedSet<Message>(), "ChatName");
        }

        [Fact]
        public async Task AddChatParticipantsHandler_ShouldThrowPublicChatRoomDoesNotExistException()
        {
            // Arrange
            var request = new AddChatParticipants(guid, new HashSet<ApplicationUser>() { _user3 });
            _readServiceMock.Setup(s => s.ExistsByIdAsync(guid)).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<PublicChatRoomDoesNotExistException>(() => _addChatPartHand.Handle(request, CancellationToken.None));
        }
        [Fact]  
        public async Task AddChatParticipantsHandler_ShouldCallUpdateOnce()
        {
            // Arrange
            var request = new AddChatParticipants(guid, new HashSet<ApplicationUser>() { _user3 });
            _readServiceMock.Setup(s => s.ExistsByIdAsync(guid)).ReturnsAsync(true);
            _repositoryMock.Setup(r => r.GetById(guid)).ReturnsAsync(_publicChatRoom);
            // Act
            await _addChatPartHand.Handle(request, CancellationToken.None);    
            // Assert
            _repositoryMock.Verify(r => r.Update(_publicChatRoom));
        }
        [Fact]
        public async Task AddChatParticipantsHandler_ShouldCallSaveAsyncOnce()
        {
            // Arrange
            var request = new AddChatParticipants(guid, new HashSet<ApplicationUser>() { _user3 });
            _readServiceMock.Setup(s => s.ExistsByIdAsync(guid)).ReturnsAsync(true);
            _repositoryMock.Setup(r => r.GetById(guid)).ReturnsAsync(_publicChatRoom);
            // Act
            await _addChatPartHand.Handle(request, CancellationToken.None);
            // Assert
            _repositoryMock.Verify(r => r.SaveAsync(CancellationToken.None));
        }
    }
}
