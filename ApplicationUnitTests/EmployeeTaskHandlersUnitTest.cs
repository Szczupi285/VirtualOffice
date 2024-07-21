using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.Commands.CalendarEventCommands;
using VirtualOffice.Application.Commands.EmployeeTaskCommands;
using VirtualOffice.Application.Commands.Handlers.CalendarEventHandlers;
using VirtualOffice.Application.Commands.Handlers.EmployeeTaskHandlers;
using VirtualOffice.Application.Exceptions.CalendarEvent;
using VirtualOffice.Application.Exceptions.EmployeeTask;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Interfaces;
using VirtualOffice.Domain.Repositories;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace ApplicationUnitTests
{
    public class EmployeeTaskHandlersUnitTest
    {
        private readonly Mock<IEmployeeTaskRepository> _repositoryMock;
        private readonly Mock<IEmployeeTaskReadService> _readServiceMock;
        private readonly AddAssignedEmployeesToEmployeeTaskHandler _AddAssgEmpHandler;
        private readonly RemoveAssignedEmployeesFromEmployeeTaskHandler _RemAssgEmpHandler;
        private readonly CreateEmployeeTaskHandler _CreEmpTaskHandler;
        private readonly DeleteEmployeeTaskHandler _DelEmpTaskHandler;
        private readonly UpdateEmployeeTaskHandler _UpdEmpTaskHandler;
        private readonly ApplicationUser _user1;
        private readonly ApplicationUser _user2;

        public EmployeeTaskHandlersUnitTest()
        {
            _repositoryMock = new Mock<IEmployeeTaskRepository>();
            _readServiceMock = new Mock<IEmployeeTaskReadService>();
            _AddAssgEmpHandler = new AddAssignedEmployeesToEmployeeTaskHandler(_repositoryMock.Object, _readServiceMock.Object);
            _RemAssgEmpHandler = new RemoveAssignedEmployeesFromEmployeeTaskHandler(_repositoryMock.Object, _readServiceMock.Object);
            _CreEmpTaskHandler = new CreateEmployeeTaskHandler(_repositoryMock.Object);
            _DelEmpTaskHandler = new DeleteEmployeeTaskHandler(_repositoryMock.Object, _readServiceMock.Object);
            _UpdEmpTaskHandler = new UpdateEmployeeTaskHandler(_repositoryMock.Object, _readServiceMock.Object);
            _user1 = new ApplicationUser(Guid.NewGuid(), "John", "Doe");
            _user2 = new ApplicationUser(Guid.NewGuid(), "Jane", "Roe");
        }

        [Fact]
        public async Task AddAssignedEmployeesToEmployeeTaskHandler_ShouldThrowEmployeeTaskDoesNotExistException()
        {
            // Arrange
            var request = new AddAssignedEmployeesToEmployeeTask(Guid.NewGuid(), new HashSet<ApplicationUser> { _user1, _user2 });
            _readServiceMock.Setup(s => s.ExistsByIdAsync(It.IsAny<Guid>())).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<EmployeeTaskDoesNotExistsException>(() => _AddAssgEmpHandler.Handle(request, CancellationToken.None));
        }
        [Fact]
        public async Task AddAssignedEmployeesToEmployeeTaskHandler_ShouldAddEmployeesAndSave_WhenEmployeeTaskExists()
        {
            // Arrange
            _readServiceMock.Setup(s => s.ExistsByIdAsync(It.IsAny<Guid>()))
                           .ReturnsAsync(true);

            var EmployeeTaskMock = new Mock<IEmployeeTask>();
            EmployeeTaskMock.Setup(c => c.AddEmployeesRange(It.IsAny<ICollection<ApplicationUser>>()));

            _repositoryMock.Setup(r => r.GetById(It.IsAny<ScheduleItemId>()))
                           .ReturnsAsync(EmployeeTaskMock.Object);

            var handler = new AddAssignedEmployeesToEmployeeTaskHandler(_repositoryMock.Object, _readServiceMock.Object);

            var request = new AddAssignedEmployeesToEmployeeTask(Guid.NewGuid(), new HashSet<ApplicationUser> { _user1, _user2 });

            // Act
            await handler.Handle(request, CancellationToken.None);

            // Assert
            EmployeeTaskMock.Verify(c => c.AddEmployeesRange(It.Is<ICollection<ApplicationUser>>(e => e == request.EmployeesToAdd)), Times.Once);
            _repositoryMock.Verify(r => r.Update(EmployeeTaskMock.Object), Times.Once);
            _repositoryMock.Verify(r => r.SaveAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
