using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.Commands.Handlers.PrivateChatRoomHandlers;
using VirtualOffice.Application.Commands.PrivateChatRoomCommands;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Repositories;

namespace ApplicationUnitTests
{
    public class PrivateChatRoomHandlersUnitTests
    {
        private readonly Mock<IPrivateChatRoomRepository> _repositoryMock;
        private readonly Mock<IPrivateChatRoomReadService> _readServiceMock;
        private readonly Mock<IUserRepository> _userRepostoryMock;
        private readonly Mock<IUserReadService> _userReadServiceMock;
        private readonly CreatePrivateChatRoomHandler _crePriChtRmHand;
        private readonly DeletePrivateChatRoomHandler _delPriChtRmHand;
        private readonly SendMessageHandler _sendMessHand;
        private readonly ApplicationUser _user1;
        private readonly ApplicationUser _user2;

        public PrivateChatRoomHandlersUnitTests()
        {
            _user1 = new ApplicationUser(Guid.NewGuid(), "John", "Doe");
            _user2 = new ApplicationUser(Guid.NewGuid(), "Jane", "Roe");
            _repositoryMock = new Mock<IPrivateChatRoomRepository>();
            _readServiceMock = new Mock<IPrivateChatRoomReadService>();
            _userRepostoryMock = new Mock<IUserRepository>();
            _userReadServiceMock = new Mock<IUserReadService>();
            _crePriChtRmHand = new CreatePrivateChatRoomHandler(_repositoryMock.Object);
            _delPriChtRmHand = new DeletePrivateChatRoomHandler(_repositoryMock.Object, _readServiceMock.Object);
            _sendMessHand = new SendMessageHandler(_repositoryMock.Object, _readServiceMock.Object,_userRepostoryMock.Object, _userReadServiceMock.Object);
        }

        [Fact]
        public async Task CreatePrivateChatRoomHandler_ShouldCallAddOnce()
        {
            // Arrange
            var request = new CreatePrivateChatRoom(new HashSet<ApplicationUser>() {_user1, _user2 }, new SortedSet<Message>());
            // Act
            await _crePriChtRmHand.Handle(request, CancellationToken.None);
            // Assert
            _repositoryMock.Verify(r => r.Add(It.IsAny<PrivateChatRoom>()));
        }
        [Fact]
        public async Task CreatePrivateChatRoomHandler_ShouldCallSaveAsyncOnce()
        {
            // Arrange
            var request = new CreatePrivateChatRoom(new HashSet<ApplicationUser>() { _user1, _user2 }, new SortedSet<Message>());
            // Act
            await _crePriChtRmHand.Handle(request, CancellationToken.None);
            // Assert
            _repositoryMock.Verify(r => r.SaveAsync(CancellationToken.None));
        }
    }
}
