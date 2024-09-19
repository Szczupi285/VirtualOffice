using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Builders.Document;
using VirtualOffice.Domain.DomainEvents.AbstractDocumentEvents;
using VirtualOffice.Domain.DomainEvents.PublicDocumentEvents;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.Document;

namespace DomainUnitTests
{
    public class PublicDocumentUnitTests
    {
        private PublicDocument _publicDocument { get; set; }

        private PublicDocumentBuilder documentBuilder = new PublicDocumentBuilder();

        public PublicDocumentUnitTests()
        {
            _publicDocument = BuildValidPublicDocument();
        }

        private PublicDocument BuildValidPublicDocument()
        {
            documentBuilder.SetId(Guid.NewGuid());
            documentBuilder.SetTitle("Valid Title");
            documentBuilder.SetContent("Valid Content");
            documentBuilder.SetCreationDetails(new ApplicationUserId(Guid.NewGuid()));
            documentBuilder.SetEligibleForRead(new List<ApplicationUserId> { new ApplicationUserId(Guid.NewGuid()) });
            documentBuilder.SetEligibleForWrite(new List<ApplicationUserId> { new ApplicationUserId(Guid.NewGuid()) });
            documentBuilder.SetAttachments(new List<DocumentFilePath> { new DocumentFilePath(@"D:\file.txt") });

            return documentBuilder.GetDocument();
        }

        #region events

        [Fact]
        public void SetTitle_ShouldRaiseDocumentTitleSetted()
        {
            PublicDocument publicDocument = BuildValidPublicDocument();
            publicDocument.SetTitle("Title");
            var Event = publicDocument.Events.OfType<DocumentTitleSetted>().Single();
        }

        [Fact]
        public void SetTitle_ShouldRaiseDocumentTitleSetted_PublicDocumentShouldEqual()
        {
            PublicDocument publicDocument = BuildValidPublicDocument();
            publicDocument.SetTitle("Title");
            var Event = publicDocument.Events.OfType<DocumentTitleSetted>().Single();
            Assert.Equal(publicDocument, Event.document);
        }

        [Fact]
        public void SetTitle_ShouldRaiseDocumentTitleSetted_TitleShouldEqual()
        {
            PublicDocument publicDocument = BuildValidPublicDocument();
            publicDocument.SetTitle("Title");
            var Event = publicDocument.Events.OfType<DocumentTitleSetted>().Single();
            Assert.Equal("Title", Event.title);
        }

        [Fact]
        public void SetContent_ShouldRaiseDocumentContentSetted()
        {
            PublicDocument publicDocument = BuildValidPublicDocument();
            publicDocument.SetContent("Content");
            var Event = publicDocument.Events.OfType<DocumentContentSetted>().Single();
        }

        [Fact]
        public void SetContent_ShouldRaiseDocumentContentSetted_PublicDocumentShouldEqual()
        {
            PublicDocument publicDocument = BuildValidPublicDocument();
            publicDocument.SetContent("Content");
            var Event = publicDocument.Events.OfType<DocumentContentSetted>().Single();
            Assert.Equal(publicDocument, Event.document);
        }

        [Fact]
        public void SetContent_ShouldRaiseDocumentContentSetted_DescriptionShouldEqual()
        {
            PublicDocument publicDocument = BuildValidPublicDocument();
            publicDocument.SetContent("Content");
            var Event = publicDocument.Events.OfType<DocumentContentSetted>().Single();
            Assert.Equal("Content", Event.content);
        }

        [Fact]
        public void AddNewAttachment_ShouldRaiseNewAttachmentAdded()
        {
            var publicDocument = BuildValidPublicDocument();
            var attachmentFilePath = new DocumentFilePath(@"C:\file.txt");

            publicDocument.AddNewAttachment(attachmentFilePath);

            var Event = publicDocument.Events.OfType<NewAttachmentAdded>().Single();
        }

        [Fact]
        public void AddNewAttachment_ShouldRaiseNewAttachmentAdded_PublicDocumentShouldEqual()
        {
            var publicDocument = BuildValidPublicDocument();
            var attachmentFilePath = new DocumentFilePath(@"C:\file.txt");

            publicDocument.AddNewAttachment(attachmentFilePath);

            var Event = publicDocument.Events.OfType<NewAttachmentAdded>().Single();
            Assert.Equal(publicDocument, Event.document);
        }

        [Fact]
        public void AddNewAttachment_ShouldRaiseNewAttachmentAdded_AttachmentFilePathShouldEqual()
        {
            var publicDocument = BuildValidPublicDocument();
            var attachmentFilePath = new DocumentFilePath(@"C:\file.txt");

            publicDocument.AddNewAttachment(attachmentFilePath);

            var Event = publicDocument.Events.OfType<NewAttachmentAdded>().Single();
            Assert.Equal(attachmentFilePath, Event.filePath);
        }

        [Fact]
        public void SettedCreationDate_ShouldRaiseEventPublicDocumentSettedCreationDate()
        {
            var publicDocument = BuildValidPublicDocument();
            var userId = new ApplicationUserId(Guid.NewGuid());

            publicDocument.SettedCreationDate(userId);

            var Events = publicDocument.Events.OfType<PublicDocumentSettedCreationDate>().ToList();
            Assert.Single(Events);
        }

        [Fact]
        public void DeleteAttachment_ShouldRaiseDeleteAttachment()
        {
            var publicDocument = BuildValidPublicDocument();

            publicDocument.DeleteAttachment(@"D:\file.txt");
            var Event = publicDocument.Events.OfType<AttachmentDeleted>().Single();
        }

        [Fact]
        public void DeleteAttachment_ShouldRaiseNewAttachmentAdded_PublicDocumentShouldEqual()
        {
            var publicDocument = BuildValidPublicDocument();
            publicDocument.DeleteAttachment(@"D:\file.txt");

            var Event = publicDocument.Events.OfType<AttachmentDeleted>().Single();
            Assert.Equal(publicDocument, Event.document);
        }

        [Fact]
        public void DeleteAttachment_ShouldRaiseNewAttachmentAdded_AttachmentFilePathShouldEqual()
        {
            var publicDocument = BuildValidPublicDocument();
            publicDocument.DeleteAttachment(@"D:\file.txt");

            var Event = publicDocument.Events.OfType<AttachmentDeleted>().Single();
            Assert.Equal(@"D:\file.txt", Event.filePath);
        }

        [Fact]
        public void SettedCreationDate_ShouldRaiseEventPublicDocumentSettedCreationDate_PublicDocumentShouldEqual()
        {
            var publicDocument = BuildValidPublicDocument();
            var userId = new ApplicationUserId(Guid.NewGuid());

            publicDocument.SettedCreationDate(userId);

            var Events = publicDocument.Events.OfType<PublicDocumentSettedCreationDate>().ToList();
            Assert.Single(Events);

            var createdEvent = Events.Single();
            Assert.Equal(publicDocument, createdEvent.document);
        }

        [Fact]
        public void SettedCreationDate_ShouldRaiseEventPublicDocumentSettedCreationDate_CreationDetailsShouldEqual()
        {
            var publicDocument = BuildValidPublicDocument();
            var userId = new ApplicationUserId(Guid.NewGuid());

            publicDocument.SettedCreationDate(userId);

            var Events = publicDocument.Events.OfType<PublicDocumentSettedCreationDate>().ToList();
            Assert.Single(Events);

            Assert.Equal(DateTime.UtcNow.Date, publicDocument._creationDetails.DocumentCreationDate.Value.Date);
            var createdEvent = Events.Single();
            Assert.Equal(userId, createdEvent.userId);
        }

        [Fact]
        public void AddEligibleForRead_ShouldRaisePublicDocumentAddedEligibleForRead()
        {
            var publicDocument = BuildValidPublicDocument();
            var userId = new ApplicationUserId(Guid.NewGuid());

            publicDocument.AddEligibleForRead(userId);

            var Events = publicDocument.Events.OfType<PublicDocumentAddedEligibleForRead>().ToList();
            Assert.Single(Events);
        }

        [Fact]
        public void AddEligibleForRead_ShouldRaisePublicDocumentAddedEligibleForRead_PublicDocumentShouldEqual()
        {
            var publicDocument = BuildValidPublicDocument();
            var userId = new ApplicationUserId(Guid.NewGuid());

            publicDocument.AddEligibleForRead(userId);

            var Events = publicDocument.Events.OfType<PublicDocumentAddedEligibleForRead>().ToList();
            Assert.Single(Events);

            var addedEvent = Events.Single();
            Assert.Equal(publicDocument, addedEvent.document);
        }

        [Fact]
        public void AddEligibleForRead_ShouldRaisePublicDocumentAddedEligibleForRead_UserIdShouldEqual()
        {
            var publicDocument = BuildValidPublicDocument();
            var userId = new ApplicationUserId(Guid.NewGuid());

            publicDocument.AddEligibleForRead(userId);

            var Events = publicDocument.Events.OfType<PublicDocumentAddedEligibleForRead>().ToList();
            Assert.Single(Events);

            var addedEvent = Events.Single();
            Assert.Equal(userId, addedEvent.userId);
        }

        [Fact]
        public void AddEligibleForWrite_ShouldRaisePublicDocumentAddedEligibleForWriteEvent()
        {
            var publicDocument = BuildValidPublicDocument();
            var userId = new ApplicationUserId(Guid.NewGuid());

            publicDocument.AddEligibleForWrite(userId);

            var Events = publicDocument.Events.OfType<PublicDocumentAddedEligibleForWrite>().ToList();
            Assert.Single(Events);
        }

        [Fact]
        public void AddEligibleForWrite_ShouldRaisePublicDocumentAddedEligibleForWriteEvent_PublicDocumentShouldEqual()
        {
            var publicDocument = BuildValidPublicDocument();
            var userId = new ApplicationUserId(Guid.NewGuid());

            publicDocument.AddEligibleForWrite(userId);

            var Events = publicDocument.Events.OfType<PublicDocumentAddedEligibleForWrite>().ToList();
            Assert.Single(Events);

            var addedEvent = Events.Single();
            Assert.Equal(publicDocument, addedEvent.document);
        }

        [Fact]
        public void AddEligibleForWrite_ShouldRaisePublicDocumentAddedEligibleForWriteEvent_UserIdShouldEqual()
        {
            var publicDocument = BuildValidPublicDocument();
            var userId = new ApplicationUserId(Guid.NewGuid());

            publicDocument.AddEligibleForWrite(userId);

            var Events = publicDocument.Events.OfType<PublicDocumentAddedEligibleForWrite>().ToList();
            Assert.Single(Events);

            var addedEvent = Events.Single();
            Assert.Equal(userId, addedEvent.userId);
        }

        #endregion events

        #region methods

        [Fact]
        public void EditTitle_ShouldSetTitle()
        {
            string newTitle = "New Title";

            _publicDocument.SetTitle(newTitle);

            Assert.Equal(newTitle, _publicDocument._title);
        }

        [Fact]
        public void EditDescription_ShouldSetContent()
        {
            string newDescription = "New Description";

            _publicDocument.SetContent(newDescription);

            Assert.Equal(newDescription, _publicDocument._content);
        }

        [Fact]
        public void AddNewAttachment_ShouldAddAttachmentToList()
        {
            DocumentFilePath attachmentFilePath = new DocumentFilePath("C:\\file.txt");

            _publicDocument.AddNewAttachment(attachmentFilePath);

            Assert.NotNull(_publicDocument._attachmentFilePaths);
            Assert.Contains(attachmentFilePath, _publicDocument._attachmentFilePaths);
        }

        [Fact]
        public void AddNewAttachmentsRange_ShouldAddAllAttachmentsToList()
        {
            var attachments = new List<DocumentFilePath>
            {
                new DocumentFilePath(@"C:\file.txt"),
                new DocumentFilePath(@"C:\fil.txt"),
                new DocumentFilePath(@"C:\fi.txt")
            };

            _publicDocument.AddNewAttachmentsRange(attachments);

            Assert.NotNull(_publicDocument._attachmentFilePaths);
            foreach (var attachmentFilePath in attachments)
            {
                Assert.Contains(attachmentFilePath, _publicDocument._attachmentFilePaths);
            }
        }

        [Fact]
        public void SettedCreationDate_ShouldSetCreationDate()
        {
            var publicDocument = new PublicDocument();
            var userId = new ApplicationUserId(Guid.NewGuid());

            publicDocument.SettedCreationDate(userId);

            var creationDetails = publicDocument._creationDetails;
            Assert.Equal(userId, creationDetails.UserId);
            Assert.Equal(DateTime.UtcNow, creationDetails.DocumentCreationDate, TimeSpan.FromSeconds(1)); // Allowing 1 second tolerance
        }

        [Fact]
        public void AddEligibleForRead_ShouldAddUserToEligibleForReadList()
        {
            var publicDocument = BuildValidPublicDocument();
            var userId = new ApplicationUserId(Guid.NewGuid());

            publicDocument.AddEligibleForRead(userId);

            Assert.Contains(userId, publicDocument._eligibleForRead);
        }

        [Fact]
        public void AddEligibleForWrite_ShouldAddUserToEligibleForWriteList()
        {
            var publicDocument = BuildValidPublicDocument();
            var userId = new ApplicationUserId(Guid.NewGuid());

            publicDocument.AddEligibleForWrite(userId);

            Assert.Contains(userId, publicDocument._eligibleForWrite);
        }

        #endregion methods
    }
}