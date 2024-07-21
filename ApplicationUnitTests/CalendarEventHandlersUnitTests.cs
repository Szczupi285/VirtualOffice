using Moq;
using VirtualOffice.Application.Commands.CalendarEventCommands;
using VirtualOffice.Application.Commands.Handlers.CalendarEventHandlers;
using VirtualOffice.Application.Exceptions.CalendarEvent;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Interfaces;
using VirtualOffice.Domain.Repositories;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace ApplicationUnitTests
{
    public class CalendarEventHandlersUnitTests
    {

        private readonly Mock<ICalendarEventRepository> _repositoryMock;
        private readonly Mock<ICalendarEventReadService> _readServiceMock;
        private readonly AddCalendarEventAssignedEmployeesHandler _AddCalEvHandler;
        private readonly DeleteCalendarEventHandler _DelCalEvHandler;
        private readonly ApplicationUser _user1;
        private readonly ApplicationUser _user2;

        public CalendarEventHandlersUnitTests()
        {
            _repositoryMock = new Mock<ICalendarEventRepository>();
            _readServiceMock = new Mock<ICalendarEventReadService>();
            _AddCalEvHandler = new AddCalendarEventAssignedEmployeesHandler(_repositoryMock.Object, _readServiceMock.Object);
            _DelCalEvHandler = new DeleteCalendarEventHandler(_repositoryMock.Object, _readServiceMock.Object);
            _user1 = new ApplicationUser(Guid.NewGuid(), "John", "Doe");
            _user2 = new ApplicationUser(Guid.NewGuid(), "Jane", "Roe");
        }

        [Fact]
        public async Task AddCalendarEventHandler_ShouldThrowCalendarEventDoesNotExistsException()
        {
            // Arrange
            var request = new AddCalendarEventAssignedEmployees(Guid.NewGuid(), new HashSet<ApplicationUser> { _user1, _user2 });
            _readServiceMock.Setup(s => s.ExistsByIdAsync(It.IsAny<Guid>())).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<CalendarEventDoesNotExistException>(() => _AddCalEvHandler.Handle(request, CancellationToken.None));
        }
        [Fact]
        public async Task AddCalendarEventHandler_ShouldAddEmployeesAndSave_WhenCalendarEventExists()
        {
            // Arrange
            _readServiceMock.Setup(s => s.ExistsByIdAsync(It.IsAny<Guid>()))
                           .ReturnsAsync(true);

            var calendarEventMock = new Mock<ICalendarEvent>();
            calendarEventMock.Setup(c => c.AddEmployeesRange(It.IsAny<ICollection<ApplicationUser>>()));

           _repositoryMock.Setup(r => r.GetById(It.IsAny<ScheduleItemId>()))
                          .ReturnsAsync(calendarEventMock.Object);

            var handler = new AddCalendarEventAssignedEmployeesHandler(_repositoryMock.Object, _readServiceMock.Object);

            var request = new AddCalendarEventAssignedEmployees(Guid.NewGuid(), new HashSet<ApplicationUser> { _user1, _user2 });

            // Act
            await handler.Handle(request, CancellationToken.None);

            // Assert
            calendarEventMock.Verify(c => c.AddEmployeesRange(It.Is<ICollection<ApplicationUser>>(e => e == request.EmployeesToAdd)), Times.Once);
            _repositoryMock.Verify(r => r.Update(calendarEventMock.Object), Times.Once);
            _repositoryMock.Verify(r => r.SaveAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
        [Fact]
        public async Task CreateCalendarEventHandler_ShouldCreateCalendarEventAndSave()
        {
            // Arrange
            var handler = new CreateCalendarEventHandler(_repositoryMock.Object);

            var request = new CreateCalendarEvent("Title", "Desc", new HashSet<ApplicationUser> { _user1, _user2 }, DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2));
            // Act
            await handler.Handle(request, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(r => r.Add(It.IsAny<ICalendarEvent>()), Times.Once);
            _repositoryMock.Verify(r => r.SaveAsync(It.IsAny<CancellationToken>()), Times.Once);    
        }
        [Fact]
        public async Task DeleteCalendarEventHandler_ShouldThrowCalendarEventDoesNotExistException()
        {

            // Arrange
            var request = new DeleteCalendarEvent(Guid.NewGuid());
            _readServiceMock.Setup(s => s.ExistsByIdAsync(It.IsAny<Guid>())).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<CalendarEventDoesNotExistException>(() => _DelCalEvHandler.Handle(request, CancellationToken.None));

        }
        [Fact]
        public async Task DeleteCalendarEventHandler_ShouldDeleteCalendarEventAndSave()
        {

            // Arrange
            Guid Id = Guid.NewGuid();
            var request = new DeleteCalendarEvent(Id);
            _readServiceMock.Setup(s => s.ExistsByIdAsync(It.IsAny<Guid>())).ReturnsAsync(true);
            // Act
            await _DelCalEvHandler.Handle(request,CancellationToken.None);
            // Assert
            _repositoryMock.Verify(r => r.Delete(Id),Times.Once);
            _repositoryMock.Verify(r => r.SaveAsync(It.IsAny<CancellationToken>()), Times.Once);


        }


    }
}