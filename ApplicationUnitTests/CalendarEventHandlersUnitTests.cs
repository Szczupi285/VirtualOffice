using MediatR;
using Moq;
using VirtualOffice.Application.Commands.CalendarEventCommands;
using VirtualOffice.Application.Commands.Handlers.CalendarEventHandlers;
using VirtualOffice.Application.Exceptions.CalendarEvent;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Repositories;

namespace ApplicationUnitTests
{
    public class CalendarEventHandlersUnitTests
    {
        private readonly Guid guid = Guid.NewGuid();
        private readonly CalendarEvent _calEv;
        private readonly Mock<ICalendarEventRepository> _repositoryMock;
        private readonly Mock<ICalendarEventReadService> _readServiceMock;
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IUserReadService> _userReadServiceMock;
        private readonly AddCalendarEventAssignedEmployeesHandler _AddCalEvHandler;
        private readonly RemoveCalendarEventAssignedEmployeesHandler _RemCalEvHandler;
        private readonly DeleteCalendarEventHandler _DelCalEvHandler;
        private readonly UpdateCalendarEventTitleHandler _UpdCalEvHandler;
        private readonly ApplicationUser _user1;
        private readonly ApplicationUser _user2;
        private readonly ApplicationUser _user3;
        private readonly ApplicationUser _user4;

        public CalendarEventHandlersUnitTests()
        {
            _repositoryMock = new Mock<ICalendarEventRepository>();
            _readServiceMock = new Mock<ICalendarEventReadService>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _userReadServiceMock = new Mock<IUserReadService>();
            _AddCalEvHandler = new AddCalendarEventAssignedEmployeesHandler(_repositoryMock.Object, _readServiceMock.Object, new Mock<IMediator>().Object,
                 _userRepositoryMock.Object, _userReadServiceMock.Object);
            _DelCalEvHandler = new DeleteCalendarEventHandler(_repositoryMock.Object, _readServiceMock.Object, new Mock<IMediator>().Object);
            _RemCalEvHandler = new RemoveCalendarEventAssignedEmployeesHandler(_repositoryMock.Object, _readServiceMock.Object);
            _UpdCalEvHandler = new UpdateCalendarEventTitleHandler(_repositoryMock.Object, _readServiceMock.Object, new Mock<IMediator>().Object);
            _user1 = new ApplicationUser(Guid.NewGuid(), "John", "Doe");
            _user2 = new ApplicationUser(Guid.NewGuid(), "Jane", "Roe");
            _user3 = new ApplicationUser(Guid.NewGuid(), "Joe", "Rane");
            _user4 = new ApplicationUser(Guid.NewGuid(), "Jack", "Dane");
            _calEv = new CalendarEvent(guid, "Title", "Description", new HashSet<ApplicationUser> { _user1, _user2 }, DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2));
        }

        [Fact]
        public async Task AddCalendarEventAssignedEmployeesHandler_ShouldThrowCalendarEventDoesNotExistsException()
        {
            // Arrange
            var request = new AddCalendarEventAssignedEmployees(guid, new HashSet<Guid> { _user1.Id, _user2.Id });
            _readServiceMock.Setup(s => s.ExistsByIdAsync(It.IsAny<Guid>())).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<CalendarEventDoesNotExistException>(() => _AddCalEvHandler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task AddCalendarEventAssignedEmployeesHandler_ShouldAddEmployees_WhenCalendarEventExists()
        {
            // Arrange
            var request = new AddCalendarEventAssignedEmployees(guid, new HashSet<Guid> { _user2.Id });

            _readServiceMock.Setup(s => s.ExistsByIdAsync(request.Id))
                           .ReturnsAsync(true);

            _repositoryMock.Setup(r => r.GetByIdAsync(request.Id, default))
                           .ReturnsAsync(_calEv);
            _userReadServiceMock.Setup(r => r.ExistsByIdAsync(_user2.Id))
                .ReturnsAsync(true);
            _userRepositoryMock.Setup(r => r.GetByIdAsync(_user2.Id, default))
                .ReturnsAsync(_user2);

            var handler = new AddCalendarEventAssignedEmployeesHandler(_repositoryMock.Object, _readServiceMock.Object,
                new Mock<IMediator>().Object, _userRepositoryMock.Object, _userReadServiceMock.Object);

            // Act
            await handler.Handle(request, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(r => r.UpdateAsync(_calEv, default), Times.Once);
        }

        [Fact]
        public async Task RemoveCalendarEventAssignedEmployeesHandler_ShouldThrowCalendarEventDoesNotExistException()
        {
            // Arrange
            var request = new RemoveCalendarEventAssignedEmployees(Guid.NewGuid(), new HashSet<ApplicationUser> { _user1 });
            _readServiceMock.Setup(s => s.ExistsByIdAsync(It.IsAny<Guid>())).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<CalendarEventDoesNotExistException>(() => _RemCalEvHandler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task RemoveCalendarEventAssignedEmployeesHandler_ShouldRemoveCalendarEventEmployees()
        {
            // Arrange
            var request = new RemoveCalendarEventAssignedEmployees(guid, new HashSet<ApplicationUser> { _user1 });

            _readServiceMock.Setup(s => s.ExistsByIdAsync(request.Id))
                           .ReturnsAsync(true);

            _repositoryMock.Setup(r => r.GetByIdAsync(request.Id, default))
                           .ReturnsAsync(_calEv);

            // Act
            await _RemCalEvHandler.Handle(request, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(r => r.UpdateAsync(_calEv, default), Times.Once);
        }

        [Fact]
        public async Task CreateCalendarEventHandler_ShouldCreateCalendarEvent()
        {
            // Arrange
            var handler = new CreateCalendarEventHandler(_repositoryMock.Object, new Mock<IMediator>().Object);

            var request = new CreateCalendarEvent("Title", "Desc", new HashSet<ApplicationUser> { _user1, _user2 }, DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2));
            // Act
            await handler.Handle(request, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<CalendarEvent>(), default), Times.Once);
        }

        [Fact]
        public async Task DeleteCalendarEventHandler_ShouldThrowCalendarEventDoesNotExistException()
        {
            // Arrange
            var request = new DeleteCalendarEvent(guid);
            _readServiceMock.Setup(s => s.ExistsByIdAsync(request.Id)).ReturnsAsync(false);
            _repositoryMock.Setup(r => r.GetByIdAsync(request.Id, default)).ReturnsAsync(_calEv);

            // Act & Assert
            await Assert.ThrowsAsync<CalendarEventDoesNotExistException>(() => _DelCalEvHandler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task DeleteCalendarEventHandler_ShouldDeleteCalendarEvent()
        {
            // Arrange

            var request = new DeleteCalendarEvent(guid);
            _readServiceMock.Setup(s => s.ExistsByIdAsync(request.Id)).ReturnsAsync(true);
            _repositoryMock.Setup(r => r.GetByIdAsync(request.Id, default)).ReturnsAsync(_calEv);
            // Act
            await _DelCalEvHandler.Handle(request, CancellationToken.None);
            // Assert
            _repositoryMock.Verify(r => r.DeleteAsync(_calEv, default), Times.Once);
        }
    }
}