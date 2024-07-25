using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Application.Commands.Handlers.PublicDocumentHandlers;
using VirtualOffice.Application.Commands.PublicDocumentCommands;
using VirtualOffice.Application.Exceptions.PublicDocument;
using VirtualOffice.Application.Services;
using VirtualOffice.Domain.Builders.Document;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Repositories;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace ApplicationUnitTests
{
    public class PublicDocumentHandlersUnitTests
    {
        private readonly Guid _guid = Guid.NewGuid();
        private readonly Mock<IPublicDocumentRepository> _repositoryMock;
        private readonly Mock<IPublicDocumentReadService> _readServiceMock;
        private readonly AddPublicDocumentAttachmentHandler _addPubDocAttHand;
        private readonly AddPublicDocumentHandler _addPubDocHand;
        private readonly DeletePublicDocumentAttachmentHandler _delPubDocAttHand;
        private readonly DeletePublicDocumentHandler _delPubDocHand;
        private readonly UpdatePublicDocumentContentHandler _updPubDocContHand;
        private readonly UpdatePublicDocumentTitleHandler _updPubDocTitHand;
        private readonly PublicDocument _publicDocument;
        private readonly ApplicationUser _user1;
        private readonly ApplicationUser _user2;
        private readonly ApplicationUser _user3;
        private readonly ApplicationUser _user4;

        public PublicDocumentHandlersUnitTests()
        {
            _user1 = new ApplicationUser(Guid.NewGuid(), "John", "Doe");
            _user2 = new ApplicationUser(Guid.NewGuid(), "Jane", "Roe");
            _user3 = new ApplicationUser(Guid.NewGuid(), "Judy", "Poe");
            _user4 = new ApplicationUser(Guid.NewGuid(), "Jennifer", "Koe");
            _repositoryMock = new Mock<IPublicDocumentRepository>();
            _readServiceMock = new Mock<IPublicDocumentReadService>();
            _addPubDocAttHand = new AddPublicDocumentAttachmentHandler(_repositoryMock.Object, _readServiceMock.Object);
            _addPubDocHand = new AddPublicDocumentHandler(_repositoryMock.Object, _readServiceMock.Object);
            _delPubDocAttHand = new DeletePublicDocumentAttachmentHandler(_repositoryMock.Object, _readServiceMock.Object);
            _delPubDocHand = new DeletePublicDocumentHandler(_repositoryMock.Object, _readServiceMock.Object);
            _updPubDocContHand = new UpdatePublicDocumentContentHandler(_repositoryMock.Object, _readServiceMock.Object);
            _updPubDocTitHand = new UpdatePublicDocumentTitleHandler(_repositoryMock.Object, _readServiceMock.Object);
            PublicDocumentBuilder builder = new PublicDocumentBuilder();
            builder.SetId(_guid);
            builder.SetContent("Content");
            builder.SetTitle("Title");
            builder.SetCreationDetails(Guid.NewGuid());
            builder.SetEligibleForRead(new HashSet<ApplicationUserId>() { _user1.Id, _user2.Id, _user3.Id });
            builder.SetEligibleForWrite(new HashSet<ApplicationUserId>() { _user2.Id, _user3.Id });
            _publicDocument = builder.GetDocument();
        }

        [Fact]
        public async Task AddPublicDocumentAttachmentHandler_ShouldThrowPublicDocumentDoesNotExistException()
        {
            // Assert
            var request = new AddPublicDocumentAttachment(Guid.NewGuid(), @"C:\SomeFold\");
            _readServiceMock.Setup(s => s.ExistsByIdAsync(Guid.NewGuid())).ReturnsAsync(false);

            // Act & Assert
            await Assert.ThrowsAsync<PublicDocumentDoesNotExistException>(() => _addPubDocAttHand.Handle(request, CancellationToken.None));
        }
        [Fact]
        public async Task AddPublicDocumentAttachmentHandler_ShouldCallUpdateOnce()
        {
            // Assert
            var request = new AddPublicDocumentAttachment(_guid, @"C:\SomeFold\");
            _readServiceMock.Setup(s => s.ExistsByIdAsync(_guid)).ReturnsAsync(true);
            _repositoryMock.Setup(r => r.GetById(_guid)).ReturnsAsync(_publicDocument);
            // Act 
            await _addPubDocAttHand.Handle(request, CancellationToken.None);
            // Assert
            _repositoryMock.Verify(r => r.Update(_publicDocument), Times.Once);
        }
        [Fact]
        public async Task AddPublicDocumentAttachmentHandler_ShouldSaveAsyncOnce()
        {
            // Assert
            var request = new AddPublicDocumentAttachment(_guid, @"C:\SomeFold\");
            _readServiceMock.Setup(s => s.ExistsByIdAsync(_guid)).ReturnsAsync(true);
            _repositoryMock.Setup(r => r.GetById(_guid)).ReturnsAsync(_publicDocument);
            // Act 
            await _addPubDocAttHand.Handle(request, CancellationToken.None);
            // Assert
            _repositoryMock.Verify(r => r.SaveAsync(CancellationToken.None), Times.Once);
        }
    }
}
