using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.Commands.EmployeeTaskCommands;
using VirtualOffice.Application.Commands.Handlers.NoteHandlers;
using VirtualOffice.Application.Commands.NoteCommands;
using VirtualOffice.Application.Exceptions.EmployeeTask;
using VirtualOffice.Application.Exceptions.Note;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Repositories;
using VirtualOffice.Domain.ValueObjects.Note;

namespace ApplicationUnitTests
{
    public class NoteHandlerUnitTest
    {
        private readonly Guid guid = Guid.NewGuid();
        private readonly Note _Note;
        private readonly Mock<INoteRepository> _repositoryMock;
        private readonly Mock<INoteReadService> _readServiceMock;
        private readonly CreateNoteHandler _createNoteHandler;
        private readonly DeleteNoteHandler _deleteNoteHandler;
        private readonly UpdateNoteHandler _updateNoteHandler;
        private readonly ApplicationUser _user1;

        public NoteHandlerUnitTest()
        {
            _user1 = new ApplicationUser(Guid.NewGuid(), "John", "Doe");
            _Note = new Note(guid, "Title", "Description", _user1.Id);
            _repositoryMock = new Mock<INoteRepository>();
            _readServiceMock = new Mock<INoteReadService>();
            _createNoteHandler = new CreateNoteHandler(_repositoryMock.Object);
            _deleteNoteHandler = new DeleteNoteHandler(_repositoryMock.Object, _readServiceMock.Object);
            _updateNoteHandler = new UpdateNoteHandler(_repositoryMock.Object, _readServiceMock.Object);
        }

        [Fact]
        public async Task CreateNoteHandler_ShouldCallAddOnce()
        {
            // Arrange
            var request = new CreateNote("Title", "Content", _user1.Id);
            // Act
            await _createNoteHandler.Handle(request, CancellationToken.None);
            // Assert
            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Note>()), Times.Once);
        }

        [Fact]
        public async Task DeleteNoteHandler_ShouldThrowNoteDoesNoteExistsException()
        {
            // Arrange
            var request = new DeleteNote(Guid.NewGuid());
            _readServiceMock.Setup(s => s.ExistsByIdAsync(It.IsAny<Guid>())).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<NoteDoesNoteExistsException>(() => _deleteNoteHandler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task DeleteNoteHandler_ShouldCallDeleteOnce()
        {
            // Arrange
            var request = new DeleteNote(guid);
            _readServiceMock.Setup(s => s.ExistsByIdAsync(guid)).ReturnsAsync(true);
            _repositoryMock.Setup(r => r.GetByIdAsync(guid)).ReturnsAsync(_Note);
            // Act
            await _deleteNoteHandler.Handle(request, CancellationToken.None);
            // Assert
            _repositoryMock.Verify(r => r.DeleteAsync(_Note), Times.Once);
        }

        [Fact]
        public async Task UpdateNoteHandler_ShouldThrowNoteDoesNoteExistsException()
        {
            // Arrange
            var request = new UpdateNote(Guid.NewGuid(), "Title", "Content");
            _readServiceMock.Setup(s => s.ExistsByIdAsync(It.IsAny<Guid>())).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<NoteDoesNoteExistsException>(() => _updateNoteHandler.Handle(request, CancellationToken.None));
        }

        [Fact]
        public async Task UpdateNoteHandler_ShouldCallUpdateOnce()
        {
            // Arrange
            var request = new UpdateNote(guid, "Title", "Content");
            _readServiceMock.Setup(s => s.ExistsByIdAsync(guid)).ReturnsAsync(true);
            _repositoryMock.Setup(r => r.GetByIdAsync(guid)).ReturnsAsync(_Note);
            // Act
            await _updateNoteHandler.Handle(request, CancellationToken.None);
            // Assert
            _repositoryMock.Verify(r => r.UpdateAsync(_Note), Times.Once);
        }
    }
}