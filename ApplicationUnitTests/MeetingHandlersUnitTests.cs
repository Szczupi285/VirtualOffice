using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.Commands.EmployeeTaskCommands;
using VirtualOffice.Application.Commands.Handlers.MeetingEventHandlers;
using VirtualOffice.Application.Commands.Handlers.MeetingHandlers;
using VirtualOffice.Application.Commands.MeetingCommands;
using VirtualOffice.Application.Exceptions.Meeting;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Interfaces;
using VirtualOffice.Domain.Repositories;
using VirtualOffice.Domain.ValueObjects.ScheduleItem;

namespace ApplicationUnitTests
{
    public class MeetingHandlersUnitTests
    {
        private readonly Mock<IMeetingRepository> _repositoryMock;
        private readonly Mock<IMeetingReadService> _readServiceMock;
        private readonly CreateMeetingHandler _CreMettHan;
        private readonly DeleteMeetingHandler _DelMettHan;
        private readonly UpdateMeetingHandler _UpdMettHan;
        private readonly ApplicationUser _user1;
        private readonly ApplicationUser _user2;

        public MeetingHandlersUnitTests()
        {
            _repositoryMock = new Mock<IMeetingRepository>();   
            _readServiceMock = new Mock<IMeetingReadService>();
            _CreMettHan = new CreateMeetingHandler(_repositoryMock.Object);
            _DelMettHan = new DeleteMeetingHandler(_repositoryMock.Object, _readServiceMock.Object);
            _UpdMettHan = new UpdateMeetingHandler(_repositoryMock.Object, _readServiceMock.Object);
            _user1 = new ApplicationUser(Guid.NewGuid(), "John", "Doe");
            _user2 = new ApplicationUser(Guid.NewGuid(), "Jane", "Roe");
        }

        [Fact]
        public async Task CreateMeetingHandler_ShouldCallAddOnce()
        {
            // Arrange
            var request = new CreateMeeting("Title", "Desc", new HashSet<ApplicationUser>() { _user1, _user2 }, DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2));
            
            // Act
            await _CreMettHan.Handle(request, CancellationToken.None);

            //Assert
            _repositoryMock.Verify(r => r.Add(It.IsAny<IMeeting>()), Times.Once());
        }
        [Fact]
        public async Task CreateMeetingHandler_ShouldCallSaveAsyncOnce()
        {
            // Arrange
            var request = new CreateMeeting("Title", "Desc", new HashSet<ApplicationUser>() { _user1, _user2 }, DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2));

            // Act
            await _CreMettHan.Handle(request, CancellationToken.None);

            //Assert
            _repositoryMock.Verify(r => r.SaveAsync(CancellationToken.None), Times.Once);
        }
        [Fact]
        public async Task DeleteMeetingHandler_ShouldThrowMeetingDoesNotExistException()
        {
            // Arrange 
            var request = new DeleteMeeting(Guid.NewGuid());
            _readServiceMock.Setup(s => s.ExistsByIdAsync(It.IsAny<Guid>())).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<MeetingDoesNotExistException>(() => _DelMettHan.Handle(request, CancellationToken.None));
        }
        [Fact]
        public async Task DeleteMeetingHandler_ShouldCallDeleteOnce()
        {
            Guid guid = Guid.NewGuid();
            // Arrange
            var request = new DeleteMeeting(guid);
            // Act
            await _DelMettHan.Handle(request, CancellationToken.None);
            _readServiceMock.Setup(s => s.ExistsByIdAsync(guid)).ReturnsAsync(true);

            // Assert
            _repositoryMock.Verify(r => r.Delete(guid), Times.Once);
        }
        [Fact]
        public async Task DeleteMeetingHandler_ShouldCallSaveAsyncOnce()
        {
            // Arrange
            Guid guid = Guid.NewGuid();
            var request = new DeleteMeeting(guid);
            // Act
            await _DelMettHan.Handle(request, CancellationToken.None);
            _readServiceMock.Setup(s => s.ExistsByIdAsync(guid)).ReturnsAsync(true);

            // Assert
            _repositoryMock.Verify(r => r.SaveAsync(CancellationToken.None), Times.Once);
        }
        [Fact]
        public async Task UpdateMeetingHandler_ShouldThrowMeetingDoesNotExistsException()
        {
            // Arrange
            Guid guid = Guid.NewGuid();
            var request = new UpdateMeeting(guid, "Title", "Desc", DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2));
            _readServiceMock.Setup(s => s.ExistsByIdAsync(guid)).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<MeetingDoesNotExistException>(() => _UpdMettHan.Handle(request, CancellationToken.None));
        }
        [Fact]
        public async Task UpdateMeetingHandler_ShouldCallUpdateOnce()
        {
            // Arrange
            var request = new UpdateMeeting(Guid.NewGuid(), "Title", "Description",
                 DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2));

            _readServiceMock.Setup(s => s.ExistsByIdAsync(request.Guid)).ReturnsAsync(true);

            var MeetingMock = new Mock<IMeeting>();
            // setup properties
            MeetingMock.SetupGet(e => e._Title).Returns("OldTitle");
            MeetingMock.SetupGet(e => e._Description).Returns("OldDescription");
            MeetingMock.SetupGet(e => e._StartDate).Returns(DateTime.UtcNow.AddDays(1));
            MeetingMock.SetupGet(e => e._EndDate).Returns(DateTime.UtcNow.AddDays(2));

            _repositoryMock.Setup(r => r.GetById(request.Guid)).ReturnsAsync(MeetingMock.Object);

            // Act
            await _UpdMettHan.Handle(request, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(r => r.Update(MeetingMock.Object), Times.Once);
        }
        [Fact]
        public async Task UpdateMeetingHandler_ShouldCallSaveAsyncOnce()
        {
            // Arrange
            var request = new UpdateMeeting(Guid.NewGuid(), "Title", "Description",
                 DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2));

            _readServiceMock.Setup(s => s.ExistsByIdAsync(request.Guid)).ReturnsAsync(true);

            var MeetingMock = new Mock<IMeeting>();
            // setup properties
            MeetingMock.SetupGet(e => e._Title).Returns("OldTitle");
            MeetingMock.SetupGet(e => e._Description).Returns("OldDescription");
            MeetingMock.SetupGet(e => e._StartDate).Returns(DateTime.UtcNow.AddDays(1));
            MeetingMock.SetupGet(e => e._EndDate).Returns(DateTime.UtcNow.AddDays(2));

            _repositoryMock.Setup(r => r.GetById(request.Guid)).ReturnsAsync(MeetingMock.Object);

            // Act
            await _UpdMettHan.Handle(request, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(r => r.SaveAsync(CancellationToken.None), Times.Once);
        }
        [Fact]
        public async Task UpdateMeetingHandler_ShouldUpdateProperties()
        {
            // Arrange
            var request = new UpdateMeeting(Guid.NewGuid(), "Title", "Description",
                 DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2));

            _readServiceMock.Setup(s => s.ExistsByIdAsync(request.Guid)).ReturnsAsync(true);

            var MeetingMock = new Mock<IMeeting>();
            // setup properties
            MeetingMock.SetupGet(e => e._Title).Returns("OldTitle");
            MeetingMock.SetupGet(e => e._Description).Returns("OldDescription");
            MeetingMock.SetupGet(e => e._StartDate).Returns(DateTime.UtcNow.AddDays(1));
            MeetingMock.SetupGet(e => e._EndDate).Returns(DateTime.UtcNow.AddDays(2));

            _repositoryMock.Setup(r => r.GetById(request.Guid)).ReturnsAsync(MeetingMock.Object);

            // Act
            await _UpdMettHan.Handle(request, CancellationToken.None);

            // Assert
            MeetingMock.Verify(m => m.SetTitle("Title"), Times.Once);
            MeetingMock.Verify(m => m.SetDescription("Description"), Times.Once);
            MeetingMock.Verify(m => m.UpdateStartDate(request.StartDate), Times.Never);
            MeetingMock.Verify(m => m.UpdateEndDate(request.EndDate), Times.Never);
        }

    }
}
