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
using VirtualOffice.Domain.Consts;
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
        public async Task AddAssignedEmployeesToEmployeeTaskHandler_ShouldAddEmployees_WhenEmployeeTaskExists()
        {
            // Arrange
            _readServiceMock.Setup(s => s.ExistsByIdAsync(It.IsAny<Guid>()))
                           .ReturnsAsync(true);

            var EmployeeTaskMock = new Mock<IEmployeeTask>();
            EmployeeTaskMock.Setup(c => c.AddEmployeesRange(It.IsAny<ICollection<ApplicationUser>>()));

            _repositoryMock.Setup(r => r.GetById(It.IsAny<ScheduleItemId>()))
                           .ReturnsAsync(EmployeeTaskMock.Object);


            var request = new AddAssignedEmployeesToEmployeeTask(Guid.NewGuid(), new HashSet<ApplicationUser> { _user1, _user2 });

            // Act
            await _AddAssgEmpHandler.Handle(request, CancellationToken.None);

            // Assert
            EmployeeTaskMock.Verify(c => c.AddEmployeesRange(It.Is<ICollection<ApplicationUser>>(e => e == request.EmployeesToAdd)), Times.Once);
        }

        [Fact]
        public async Task AddAssignedEmployeesToEmployeeTaskHandler_ShouldUpdateEmployeeTask_WhenEmployeeTaskExists()
        {
            // Arrange
            _readServiceMock.Setup(s => s.ExistsByIdAsync(It.IsAny<Guid>()))
                           .ReturnsAsync(true);

            var EmployeeTaskMock = new Mock<IEmployeeTask>();
            EmployeeTaskMock.Setup(c => c.AddEmployeesRange(It.IsAny<ICollection<ApplicationUser>>()));

            _repositoryMock.Setup(r => r.GetById(It.IsAny<ScheduleItemId>()))
                           .ReturnsAsync(EmployeeTaskMock.Object);


            var request = new AddAssignedEmployeesToEmployeeTask(Guid.NewGuid(), new HashSet<ApplicationUser> { _user1, _user2 });

            // Act
            await _AddAssgEmpHandler.Handle(request, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(r => r.Update(EmployeeTaskMock.Object), Times.Once);
        }
        [Fact]
        public async Task AddAssignedEmployeesToEmployeeTaskHandler_ShouldSaveEmployeeTask()
        {
            // Arrange
            _readServiceMock.Setup(s => s.ExistsByIdAsync(It.IsAny<Guid>()))
                           .ReturnsAsync(true);

            var EmployeeTaskMock = new Mock<IEmployeeTask>();
            EmployeeTaskMock.Setup(c => c.AddEmployeesRange(It.IsAny<ICollection<ApplicationUser>>()));

            _repositoryMock.Setup(r => r.GetById(It.IsAny<ScheduleItemId>()))
                           .ReturnsAsync(EmployeeTaskMock.Object);


            var request = new AddAssignedEmployeesToEmployeeTask(Guid.NewGuid(), new HashSet<ApplicationUser> { _user1, _user2 });

            // Act
            await _AddAssgEmpHandler.Handle(request, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(r => r.SaveAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
        [Fact]
        public async Task RemoveAssignedEmployeesFromEmployeeTaskHandler_ShouldThrowEmployeeTaskDoesNotExistsException()
        {
            // Arrange 
            var request = new RemoveAssignedEmployeesFromEmployeeTask(Guid.NewGuid(), new HashSet<ApplicationUser>(){ _user1, _user2 });
            _readServiceMock.Setup(s => s.ExistsByIdAsync(It.IsAny<Guid>())).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<EmployeeTaskDoesNotExistsException>(() => _RemAssgEmpHandler.Handle(request, CancellationToken.None));
        }
        [Fact]
        public async Task RemoveAssignedEmployeesFromEmployeeTaskHandler_ShouldRemoveEmployees()
        {
            // Arrange
            _readServiceMock.Setup(s => s.ExistsByIdAsync(It.IsAny<Guid>()))
                           .ReturnsAsync(true);

            var EmployeeTaskMock = new Mock<IEmployeeTask>();
            EmployeeTaskMock.Setup(c => c.RemoveEmployeesRange(It.IsAny<ICollection<ApplicationUser>>()));

            _repositoryMock.Setup(r => r.GetById(It.IsAny<ScheduleItemId>()))
                           .ReturnsAsync(EmployeeTaskMock.Object);


            var request = new RemoveAssignedEmployeesFromEmployeeTask(Guid.NewGuid(), new HashSet<ApplicationUser> { _user1, _user2 });

            // Act
            await _RemAssgEmpHandler.Handle(request, CancellationToken.None);

            // Assert
            EmployeeTaskMock.Verify(c => c.RemoveEmployeesRange(It.Is<ICollection<ApplicationUser>>(e => e == request.EmployeesToRemove)), Times.Once);
        }
        [Fact]
        public async Task RemoveAssignedEmployeesFromEmployeeTaskHandler_ShouldUpdate()
        {
            // Arrange
            _readServiceMock.Setup(s => s.ExistsByIdAsync(It.IsAny<Guid>()))
                           .ReturnsAsync(true);

            var EmployeeTaskMock = new Mock<IEmployeeTask>();
            EmployeeTaskMock.Setup(c => c.RemoveEmployeesRange(It.IsAny<ICollection<ApplicationUser>>()));

            _repositoryMock.Setup(r => r.GetById(It.IsAny<ScheduleItemId>()))
                           .ReturnsAsync(EmployeeTaskMock.Object);


            var request = new RemoveAssignedEmployeesFromEmployeeTask(Guid.NewGuid(), new HashSet<ApplicationUser> { _user1, _user2 });

            // Act
            await _RemAssgEmpHandler.Handle(request, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(r => r.Update(EmployeeTaskMock.Object), Times.Once);
        }
        [Fact]
        public async Task RemoveAssignedEmployeesFromEmployeeTaskHandler_ShouldSave()
        {
            // Arrange
            _readServiceMock.Setup(s => s.ExistsByIdAsync(It.IsAny<Guid>()))
                           .ReturnsAsync(true);

            var EmployeeTaskMock = new Mock<IEmployeeTask>();
            EmployeeTaskMock.Setup(c => c.RemoveEmployeesRange(It.IsAny<ICollection<ApplicationUser>>()));

            _repositoryMock.Setup(r => r.GetById(It.IsAny<ScheduleItemId>()))
                           .ReturnsAsync(EmployeeTaskMock.Object);


            var request = new RemoveAssignedEmployeesFromEmployeeTask(Guid.NewGuid(), new HashSet<ApplicationUser> { _user1, _user2 });

            // Act
            await _RemAssgEmpHandler.Handle(request, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(r => r.SaveAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
        [Fact]
        public async Task CreateEmployeeTaskHandler_ShouldAddEmployeeTask()
        {
            // Arrange
            var request = new CreateEmployeeTask("Title", "Desc", new HashSet<ApplicationUser>() { _user1, _user2 },
                DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2), EmployeeTaskPriorityEnum.Low);
            // Act
            await _CreEmpTaskHandler.Handle(request, CancellationToken.None);
            // Assert
            _repositoryMock.Verify(r => r.Add(It.IsAny<IEmployeeTask>()), Times.Once);

        }
        [Fact]
        public async Task CreateEmployeeTaskHandler_ShouldSaveEmployeeTask()
        {
            // Arrange
            var request = new CreateEmployeeTask("Title", "Desc", new HashSet<ApplicationUser>() { _user1, _user2 },
                DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2), EmployeeTaskPriorityEnum.Low);
            // Act
            await _CreEmpTaskHandler.Handle(request, CancellationToken.None);
            // Assert
            _repositoryMock.Verify(r => r.SaveAsync(CancellationToken.None), Times.Once);

        }
        [Fact]
        public async Task DeleteEmployeeTaskHandler_ShouldThrowEmployeeTaskDoesNotExistsException()
        {
            // Arrange
            var request = new DeleteEmployeeTask(Guid.NewGuid());
            _readServiceMock.Setup(s => s.ExistsByIdAsync(It.IsAny<Guid>())).ReturnsAsync(false);

            // Act & Assert
            await  Assert.ThrowsAsync<EmployeeTaskDoesNotExistsException>(() => _DelEmpTaskHandler.Handle(request, CancellationToken.None));
        }
        [Fact]
        public async Task DeleteEmployeeTaskHandler_ShouldCallDeleteOnce()
        {
            // Arrange
            var request = new DeleteEmployeeTask(Guid.NewGuid());
            _readServiceMock.Setup(s => s.ExistsByIdAsync(It.IsAny<Guid>())).ReturnsAsync(true);

            // Act
            await _DelEmpTaskHandler.Handle(request, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(r => r.Delete(It.IsAny<ScheduleItemId>()), Times.Once);
        }
        [Fact]
        public async Task DeleteEmployeeTaskHandler_ShouldCallSaveOnce()
        {
            // Arrange
            var request = new DeleteEmployeeTask(Guid.NewGuid());
            _readServiceMock.Setup(s => s.ExistsByIdAsync(It.IsAny<Guid>())).ReturnsAsync(true);

            // Act
            await _DelEmpTaskHandler.Handle(request, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(r => r.SaveAsync(CancellationToken.None), Times.Once);
        }
    }
}
