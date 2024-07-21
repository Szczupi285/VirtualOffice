using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.Commands.Handlers.MeetingEventHandlers;
using VirtualOffice.Application.Commands.Handlers.MeetingHandlers;
using VirtualOffice.Application.Commands.MeetingCommands;
using VirtualOffice.Application.Exceptions.Meeting;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Interfaces;
using VirtualOffice.Domain.Repositories;

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
            Guid guid = Guid.NewGuid();
            // Arrange
            var request = new DeleteMeeting(guid);
            // Act
            await _DelMettHan.Handle(request, CancellationToken.None);
            _readServiceMock.Setup(s => s.ExistsByIdAsync(guid)).ReturnsAsync(true);

            // Assert
            _repositoryMock.Verify(r => r.SaveAsync(CancellationToken.None), Times.Once);
        }

    }
}
