using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.Commands.Handlers.UserHandlers;
using VirtualOffice.Application.Commands.UserCommands;
using VirtualOffice.Application.Exceptions.PublicDocument;
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
            _repositoryMock.Verify(r => r.Update(It.IsAny<ApplicationUser>()));
        }
        [Fact]
        public async Task CreateUserHandler_ShouldCallSaveAsyncOnce()
        {
            // Arrange
            var request = new CreateUser("Name", "Surname", PermissionsEnum.None);
            // Act
            await _creUserHand.Handle(request, CancellationToken.None);
            // Assert
            _repositoryMock.Verify(r => r.SaveAsync(CancellationToken.None));
        }
        
    }
}
