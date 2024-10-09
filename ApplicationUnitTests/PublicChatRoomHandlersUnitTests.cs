using Moq;
using VirtualOffice.Application.Commands.Handlers.PublicChatRoomHandlers;
using VirtualOffice.Application.Commands.PublicChatRoomCommands;
using VirtualOffice.Application.Exceptions.ApplicationUser;
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
        private readonly Mock<IUserReadService> _userReadServiceMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
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
        private readonly ApplicationUser _user4;

        public PublicChatRoomHandlersUnitTests()
        {
            _user1 = new ApplicationUser(Guid.NewGuid(), "John", "Doe");
            _user2 = new ApplicationUser(Guid.NewGuid(), "Jane", "Roe");
            _user3 = new ApplicationUser(Guid.NewGuid(), "Judy", "Poe");
            _user4 = new ApplicationUser(Guid.NewGuid(), "Jennifer", "Koe");
            _repositoryMock = new Mock<IPublicChatRoomRepository>();
            _readServiceMock = new Mock<IPublicChatRoomReadService>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _userReadServiceMock = new Mock<IUserReadService>();

            _addChatPartHand = new AddChatParticipantsHandler(_repositoryMock.Object, _readServiceMock.Object);
            _crePubChatRoomHand = new CreatePublicChatRoomHandler(_repositoryMock.Object);
            _delChatPartHand = new DeleteChatParticipantsHandler(_repositoryMock.Object, _readServiceMock.Object);
            _delPubChatRoomHand = new DeletePublicChatRoomHandler(_repositoryMock.Object, _readServiceMock.Object);
            _sendPubMessHand = new SendPublicMessageHandler(_repositoryMock.Object, _readServiceMock.Object,
                _userReadServiceMock.Object, _userRepositoryMock.Object);
            _updPubChatRoomNameHand = new UpdatePublicChatRoomNameHandler(_repositoryMock.Object, _readServiceMock.Object);
            _publicChatRoom = new PublicChatRoom(guid, new HashSet<ApplicationUser>() { _user1, _user2, _user4 }, new SortedSet<Message>(), "ChatName");
        }

        [Fact]
        public async Task AddChatParticipantsHandler_ShouldThrowPublicChatRoomDoesNotExistException()
        {
            // Arrange
            var request = new AddChatParticipants(guid, new HashSet<ApplicationUser>() { _user3 });
            _readServiceMock.Setup(s => s.ExistsByIdAsync(request.Id)).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<PublicChatRoomDoesNotExistException>(() => _addChatPartHand.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task AddChatParticipantsHandler_ShouldCallUpdateOnce()
        {
            // Arrange
            var request = new AddChatParticipants(guid, new HashSet<ApplicationUser>() { _user3 });
            _readServiceMock.Setup(s => s.ExistsByIdAsync(request.Id)).ReturnsAsync(true);
            _repositoryMock.Setup(r => r.GetByIdAsync(request.Id, default)).ReturnsAsync(_publicChatRoom);
            // Act
            await _addChatPartHand.Handle(request, CancellationToken.None);
            // Assert
            _repositoryMock.Verify(r => r.UpdateAsync(_publicChatRoom, default));
        }

        [Fact]
        public async Task CreatePublicChatRoomHandler_ShouldCallAddOnce()
        {
            // Arrange
            var request = new CreatePublicChatRoom(new HashSet<ApplicationUser>() { _user3, _user1 }, new SortedSet<Message>(), "NewChatName");
            // Act
            await _crePubChatRoomHand.Handle(request, CancellationToken.None);
            // Assert
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<PublicChatRoom>(), default), Times.Once());
        }

        [Fact]
        public async Task DeleteChatParticipantsHandler_ShouldThrowPublicChatRoomDoesNotExistException()
        {
            // Arrange
            var request = new DeleteChatParticipants(Guid.NewGuid(), new HashSet<ApplicationUser>() { _user3 });
            _readServiceMock.Setup(s => s.ExistsByIdAsync(request.Id)).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<PublicChatRoomDoesNotExistException>(() => _delChatPartHand.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task DeleteChatParticipantsHandler_ShouldCallUpdateOnce()
        {
            // Arrange
            var request = new DeleteChatParticipants(guid, new HashSet<ApplicationUser>() { _user2 });
            _readServiceMock.Setup(s => s.ExistsByIdAsync(request.Id)).ReturnsAsync(true);
            _repositoryMock.Setup(r => r.GetByIdAsync(request.Id, default)).ReturnsAsync(_publicChatRoom);
            // Act
            await _delChatPartHand.Handle(request, CancellationToken.None);
            // Assert
            _repositoryMock.Verify(r => r.UpdateAsync(_publicChatRoom, default), Times.Once());
        }

        [Fact]
        public async Task DeletePublicChatRoomHandler_ShouldThrowPublicChatRoomDoesNotExistException()
        {
            // Arrange
            var request = new DeletePublicChatRoom(Guid.NewGuid());
            _readServiceMock.Setup(s => s.ExistsByIdAsync(Guid.NewGuid())).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<PublicChatRoomDoesNotExistException>(() => _delPubChatRoomHand.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task DeletePublicChatRoomHandler_ShouldCallDeleteOnce()
        {
            // Arrange
            var request = new DeletePublicChatRoom(_publicChatRoom.Id);
            _readServiceMock.Setup(s => s.ExistsByIdAsync(request.Id)).ReturnsAsync(true);
            _repositoryMock.Setup(r => r.GetByIdAsync(request.Id, default)).ReturnsAsync(_publicChatRoom);
            // Act
            await _delPubChatRoomHand.Handle(request, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(r => r.DeleteAsync(_publicChatRoom, default), Times.Once());
        }

        [Fact]
        public async Task SendPublicMessageHandler_ShouldThrowPublicChatRoomDoesNotExistException()
        {
            // Arrange
            var request = new SendPublicMessage(Guid.NewGuid(), Guid.NewGuid(), "content");
            _readServiceMock.Setup(s => s.ExistsByIdAsync(Guid.NewGuid())).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<PublicChatRoomDoesNotExistException>(() => _sendPubMessHand.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task SendPublicMessageHandler_ShouldThrowUserDoesNotExistException()
        {
            // Arrange
            var request = new SendPublicMessage(guid, Guid.NewGuid(), "content");
            _readServiceMock.Setup(s => s.ExistsByIdAsync(guid)).ReturnsAsync(true);
            _userReadServiceMock.Setup(us => us.ExistsByIdAsync(Guid.NewGuid())).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<UserDoesNotExistException>(() => _sendPubMessHand.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task SendPublicMessageHandler_ShouldCallUpdateOnce()
        {
            // Arrange
            var request = new SendPublicMessage(guid, _user1.Id, "content");
            _readServiceMock.Setup(s => s.ExistsByIdAsync(request.ChatRoomId)).ReturnsAsync(true);
            _userReadServiceMock.Setup(us => us.ExistsByIdAsync(request.UserId)).ReturnsAsync(true);
            _repositoryMock.Setup(r => r.GetByIdAsync(request.ChatRoomId, default)).ReturnsAsync(_publicChatRoom);
            _userRepositoryMock.Setup(ur => ur.GetByIdAsync(request.UserId)).ReturnsAsync(_user1);
            // Act
            await _sendPubMessHand.Handle(request, CancellationToken.None);
            // Assert
            _repositoryMock.Verify(r => r.UpdateAsync(_publicChatRoom, default), Times.Once);
        }

        [Fact]
        public async Task UpdatePublicChatRoomNameHandler_ShouldThrowPublicChatRoomDoesNotExistException()
        {
            // Arrange
            var request = new UpdatePublicChatRoomName(Guid.NewGuid(), "NewName");
            _readServiceMock.Setup(s => s.ExistsByIdAsync(Guid.NewGuid())).ReturnsAsync(false);
            // Act & Assert
            await Assert.ThrowsAsync<PublicChatRoomDoesNotExistException>(() => _updPubChatRoomNameHand.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task UpdatePublicChatRoomNameHandler_ShouldCallUpdateOnce()
        {
            // Arrange
            var request = new UpdatePublicChatRoomName(guid, "NewName");
            _readServiceMock.Setup(s => s.ExistsByIdAsync(request.Id)).ReturnsAsync(true);
            _repositoryMock.Setup(r => r.GetByIdAsync(request.Id, default)).ReturnsAsync(_publicChatRoom);
            // Act
            await _updPubChatRoomNameHand.Handle(request, CancellationToken.None);
            // Assert
            _repositoryMock.Verify(r => r.UpdateAsync(_publicChatRoom, default), Times.Once);
        }
    }
}