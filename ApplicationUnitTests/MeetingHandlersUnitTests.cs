using Moq;
using VirtualOffice.Application.Commands.Handlers.MeetingEventHandlers;
using VirtualOffice.Application.Commands.Handlers.MeetingHandlers;
using VirtualOffice.Application.Commands.MeetingCommands;
using VirtualOffice.Application.Exceptions.Meeting;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Repositories;

namespace ApplicationUnitTests
{
    public class MeetingHandlersUnitTests
    {
        private readonly Guid guid = Guid.NewGuid();
        private readonly Meeting _meet;
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
            _meet = new Meeting(guid, "Title", "Description", 
                new HashSet<ApplicationUser>() { _user1, _user2 }, DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2));
        }

        [Fact]
        public async Task CreateMeetingHandler_ShouldCallAddOnce()
        {
            // Arrange
            var request = new CreateMeeting("Title", "Desc", new HashSet<ApplicationUser>() { _user1, _user2 }, DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2));
            
            // Act
            await _CreMettHan.Handle(request, CancellationToken.None);

            //Assert
            _repositoryMock.Verify(r => r.Add(It.IsAny<Meeting>()), Times.Once());
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
            // Arrange
            Guid guid = Guid.NewGuid();
            var request = new DeleteMeeting(guid);
            _readServiceMock.Setup(s => s.ExistsByIdAsync(guid)).ReturnsAsync(true);
            // Act
            await _DelMettHan.Handle(request, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(r => r.Delete(guid), Times.Once);
        }
        [Fact]
        public async Task DeleteMeetingHandler_ShouldCallSaveAsyncOnce()
        {
            // Arrange
            Guid guid = Guid.NewGuid();
            var request = new DeleteMeeting(guid);
            _readServiceMock.Setup(s => s.ExistsByIdAsync(guid)).ReturnsAsync(true);
            // Act
            await _DelMettHan.Handle(request, CancellationToken.None);

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

            _repositoryMock.Setup(r => r.GetById(request.Guid)).ReturnsAsync(_meet);

            // Act
            await _UpdMettHan.Handle(request, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(r => r.Update(_meet), Times.Once);
        }
        [Fact]
        public async Task UpdateMeetingHandler_ShouldCallSaveAsyncOnce()
        {
            // Arrange
            var request = new UpdateMeeting(Guid.NewGuid(), "Title", "Description",
                 DateTime.UtcNow.AddDays(1), DateTime.UtcNow.AddDays(2));

            _readServiceMock.Setup(s => s.ExistsByIdAsync(request.Guid)).ReturnsAsync(true);

            _repositoryMock.Setup(r => r.GetById(request.Guid)).ReturnsAsync(_meet);

            // Act
            await _UpdMettHan.Handle(request, CancellationToken.None);

            // Assert
            _repositoryMock.Verify(r => r.SaveAsync(CancellationToken.None), Times.Once);
        }
    }
}
