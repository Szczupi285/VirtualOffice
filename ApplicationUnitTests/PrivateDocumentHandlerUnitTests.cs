﻿using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.Commands.Handlers.PrivateDocumentHandler;
using VirtualOffice.Application.Commands.PrivateChatRoomCommands;
using VirtualOffice.Application.Commands.PrivateDocumentCommands;
using VirtualOffice.Application.Exceptions.PrivateChatRoom;
using VirtualOffice.Application.Exceptions.PrivateDocument;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Builders.Document;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Repositories;
using VirtualOffice.Domain.ValueObjects.Document;

namespace ApplicationUnitTests
{
    public class PrivateDocumentHandlerUnitTests
    {
        private readonly Guid _guid = Guid.NewGuid();
        private readonly PrivateDocument _privateDocument;
        private readonly Mock<IPrivateDocumentRepository> _repositoryMock;
        private readonly Mock<IPrivateDocumentReadService> _readServiceMock;
        private readonly AddPrivateDocumentAttachmentHandler _addPrivDocAttHand;
        private readonly CreatePrivateDocumentHandler _crePrivDocHand;
        private readonly DeletePrivateDocumentAttachmentHandler _delPrivDocAttHand;
        private readonly DeletePrivateDocumentHandler _delPrivDocHand;
        private readonly UpdatePrivateDocumentContentHandler _updPrivDocConHand;
        private readonly UpdatePrivateDocumentTitleHandler _updPrivDocTitle;

        public PrivateDocumentHandlerUnitTests()
        {
            _repositoryMock = new Mock<IPrivateDocumentRepository>();
            _readServiceMock = new Mock<IPrivateDocumentReadService>();
            _addPrivDocAttHand = new AddPrivateDocumentAttachmentHandler(_repositoryMock.Object, _readServiceMock.Object);
            _crePrivDocHand = new CreatePrivateDocumentHandler(_repositoryMock.Object);
            _delPrivDocAttHand = new DeletePrivateDocumentAttachmentHandler(_repositoryMock.Object, _readServiceMock.Object);
            _delPrivDocHand = new DeletePrivateDocumentHandler(_repositoryMock.Object, _readServiceMock.Object);
            _updPrivDocConHand = new UpdatePrivateDocumentContentHandler(_repositoryMock.Object, _readServiceMock.Object);
            _updPrivDocTitle = new UpdatePrivateDocumentTitleHandler(_repositoryMock.Object, _readServiceMock.Object);
            PrivateDocumentBuilder builder = new PrivateDocumentBuilder();
            builder.SetId(_guid);
            builder.SetContent("content");
            builder.SetTitle("title");
            builder.SetAttachments(new List<DocumentFilePath>() { @"C:\FolderFilePath\" });
            _privateDocument = builder.GetDocument();
        }

        [Fact]
        public async Task AddPrivateDocumentAttachmentHandler_ShouldThrowPrivateDocumentDoesNotExistException()
        {
            // Arrange
            var request = new AddPrivateDocumentAttachment(Guid.NewGuid(), @"C:\newFolder\smth");
            _readServiceMock.Setup(s => s.ExistsByIdAsync(Guid.NewGuid())).ReturnsAsync(false);
            // Act & Assert
            await Assert.ThrowsAsync<PrivateDocumentDoesNotExistException>(() => _addPrivDocAttHand.Handle(request, CancellationToken.None));
        }
        [Fact]
        public async Task AddPrivateDocumentAttachmentHandler_ShouldCallUpdateOnce()
        {

            // Arrange
            var request = new AddPrivateDocumentAttachment(_guid, @"C:\newFolder\smth");
            _readServiceMock.Setup(s => s.ExistsByIdAsync(_guid)).ReturnsAsync(true);
            _repositoryMock.Setup(r => r.GetById(_guid)).ReturnsAsync(_privateDocument);
            // Act
            await _addPrivDocAttHand.Handle(request, CancellationToken.None);
            // Assert
            _repositoryMock.Verify(r => r.Update(_privateDocument), Times.Once);
        }
        [Fact]
        public async Task AddPrivateDocumentAttachmentHandler_ShouldCallSaveAsyncOnce()
        {
            // Arrange
            var request = new AddPrivateDocumentAttachment(_guid, @"C:\newFolder\smth");
            _readServiceMock.Setup(s => s.ExistsByIdAsync(_guid)).ReturnsAsync(true);
            _repositoryMock.Setup(r => r.GetById(_guid)).ReturnsAsync(_privateDocument);
            // Act
            await _addPrivDocAttHand.Handle(request, CancellationToken.None);
            // Assert
            _repositoryMock.Verify(r => r.SaveAsync(CancellationToken.None), Times.Once);
        }


    }
}
