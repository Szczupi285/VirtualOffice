using Moq;
using VirtualOffice.Application.Commands.Handlers.UserHandlers;
using VirtualOffice.Application.Commands.UserCommands;
using VirtualOffice.Application.Exceptions.ApplicationUser;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Repositories;

namespace ApplicationUnitTests
{
    public class ApplicationUserHandlers
    {
        private readonly Guid _guid = Guid.NewGuid();
        private readonly Mock<IUserRepository> _repositoryMock;
        private readonly Mock<IUserReadService> _readServiceMock;
        private readonly CreateUserHandler _creUserHand;
        private readonly DeleteUserHandler _delUserHand;
        private readonly UpdateUserHandler _UpdUserHand;
        private readonly ApplicationUser _user1;

        public ApplicationUserHandlers()
        {
            _user1 = new ApplicationUser(_guid, "John", "Doe");
            _repositoryMock = new Mock<IUserRepository>();
            _readServiceMock = new Mock<IUserReadService>();
            _creUserHand = new CreateUserHandler(_repositoryMock.Object);
            _delUserHand = new DeleteUserHandler(_repositoryMock.Object, _readServiceMock.Object);
            _UpdUserHand = new UpdateUserHandler(_repositoryMock.Object, _readServiceMock.Object);
        }

        [Fact]
        public async Task CreateUserHandler_ShouldCallAddOnce()
        {
            // Arrange
            var request = new CreateUser("Name", "Surname", PermissionsEnum.None);
            // Act
            await _creUserHand.Handle(request, CancellationToken.None);
            // Assert
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<ApplicationUser>()), Times.Once);
        }

        [Fact]
        public async Task DeleteUserHandler_ShouldThrowUserDoesNotExistException()
        {
            // Arrange
            var request = new DeleteUser(Guid.NewGuid());
            _readServiceMock.Setup(s => s.ExistsByIdAsync(It.IsAny<Guid>())).ReturnsAsync(false);
            // Act & Assert
            await Assert.ThrowsAsync<UserDoesNotExistException>(() => _delUserHand.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task DeleteUserHandler_ShouldCallDeleteOnce()
        {
            // Arrange
            var request = new DeleteUser(_guid);
            _readServiceMock.Setup(s => s.ExistsByIdAsync(request.Id)).ReturnsAsync(true);
            _repositoryMock.Setup(r => r.GetByIdAsync(request.Id)).ReturnsAsync(_user1);
            // Act
            await _delUserHand.Handle(request, CancellationToken.None);
            // Assert
            _repositoryMock.Verify(r => r.DeleteAsync(_user1), Times.Once);
        }

        [Fact]
        public async Task UpdateUserHandler_ShouldThrowUserDoesNotExistException()
        {
            // Arrange
            var request = new UpdateUser(Guid.NewGuid(), "NewName", "NewSurname");
            _readServiceMock.Setup(s => s.ExistsByIdAsync(It.IsAny<Guid>())).ReturnsAsync(false);
            // Act & Assert
            await Assert.ThrowsAsync<UserDoesNotExistException>(() => _UpdUserHand.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task UpdateUserHandler_ShouldCallUpdateOnce()
        {
            // Arrange
            var request = new UpdateUser(_guid, "NewName", "NewSurname");
            _readServiceMock.Setup(s => s.ExistsByIdAsync(request.Id)).ReturnsAsync(true);
            _repositoryMock.Setup(r => r.GetByIdAsync(request.Id)).ReturnsAsync(_user1);
            // Act
            await _UpdUserHand.Handle(request, CancellationToken.None);
            // Assert
            _repositoryMock.Verify(r => r.UpdateAsync(_user1), Times.Once);
        }
    }
}