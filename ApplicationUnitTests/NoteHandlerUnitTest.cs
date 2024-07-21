using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.Commands.Handlers.NoteHandlers;
using VirtualOffice.Application.Commands.NoteCommands;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Repositories;

namespace ApplicationUnitTests
{
    public class NoteHandlerUnitTest
    {
        private readonly Note _Note;
        private readonly Mock<INoteRepository> _repositoryMock;
        private readonly Mock<INoteReadService> _readServiceMock;
        private readonly CreateNoteHandler _createNoteHandler;
        private readonly DeleteNoteHandler _deleteNoteHandler;
        private readonly UpdateNoteHandler _UpdateNoteHandler;
        private readonly ApplicationUser _user1;

        public NoteHandlerUnitTest()
        {
            _user1 = new ApplicationUser(Guid.NewGuid(), "John", "Doe");
            _Note = new Note(Guid.NewGuid(), "Title", "Description", _user1);
            _repositoryMock = new Mock<INoteRepository>();
            _readServiceMock = new Mock<INoteReadService>();
            _createNoteHandler = new CreateNoteHandler(_repositoryMock.Object);
            _deleteNoteHandler = new DeleteNoteHandler(_repositoryMock.Object, _readServiceMock.Object);
            _UpdateNoteHandler = new UpdateNoteHandler(_repositoryMock.Object, _readServiceMock.Object);
        }

        [Fact]
        public async Task CreateNoteHandler_ShouldCallAddOnce()
        {
            // Arrange
            var request = new CreateNote("Title", "Content", _user1);
            // Act
            await _createNoteHandler.Handle(request, CancellationToken.None);
            // Assert
            _repositoryMock.Verify(r => r.Add(It.IsAny<Note>()), Times.Once);
        }
        [Fact]
        public async Task CreateNoteHandler_ShouldCallSaveAsyncOnce()
        {
            // Arrange
            var request = new CreateNote("Title", "Content", _user1);
            // Act
            await _createNoteHandler.Handle(request, CancellationToken.None);
            // Assert
            _repositoryMock.Verify(r => r.SaveAsync(CancellationToken.None), Times.Once);
        }
    }
}
