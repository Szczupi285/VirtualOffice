using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Builders.Document;
using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.DomainEvents;
using VirtualOffice.Domain.DomainEvents.AbstractDocumentEvents;
using VirtualOffice.Domain.DomainEvents.PublicDocumentEvents;
using VirtualOffice.Domain.DomainEvents.ScheduleItemEvents;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.BuilderExceptions;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.Document;

namespace DomainUnitTests
{
    public class PublicDocumentBuilderUnitTests
    {
        PublicDocument _publicDocument {  get; set; }

        PublicDocumentBuilder documentBuilder = new PublicDocumentBuilder();
        Guid id = Guid.NewGuid();
        string content = "Sample content";
        string title = "Sample title";
        ApplicationUserId creationUserId = Guid.NewGuid();
        List<ApplicationUserId> eligibleForRead = new List<ApplicationUserId> { Guid.NewGuid()};
        List<ApplicationUserId> eligibleForWrite = new List<ApplicationUserId> { Guid.NewGuid() };
        List<DocumentFilePath> attachmentFilePaths = new List<DocumentFilePath> { new DocumentFilePath(@"C:\") };
        AbstractDocument previousVersion = new PublicDocument();

        public PublicDocumentBuilderUnitTests()
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

            return documentBuilder.GetDocument();
        }

        #region properties
        [Fact]
        public void PublicDocumentBuilder_NoPropertiesSet_ShouldThrowInvalidPublicDocumentBuild()
        {
            Assert.Throws<InvalidPublicDocumentBuild>(() => documentBuilder.GetDocument());
        }
        [Fact]
        public void PublicDocumentBuilder_OnlyIdSet_ShouldThrowInvalidPublicDocumentBuild()
        {
            documentBuilder.SetId(id);
            Assert.Throws<InvalidPublicDocumentBuild>(() => documentBuilder.GetDocument());
        }
        [Fact]
        public void PublicDocumentBuilder_OnlyContentSet_ShouldThrowInvalidPublicDocumentBuild()
        {
            documentBuilder.SetContent(content);
            Assert.Throws<InvalidPublicDocumentBuild>(() => documentBuilder.GetDocument());
        }
        [Fact]
        public void PublicDocumentBuilder_OnlyTitleSet_ShouldThrowInvalidPublicDocumentBuild()
        {
            documentBuilder.SetTitle(title);
            Assert.Throws<InvalidPublicDocumentBuild>(() => documentBuilder.GetDocument());
        }
        [Fact]
        public void PublicDocumentBuilder_OnlyCreationDetailsSet_ShouldThrowInvalidPublicDocumentBuild()
        {
            documentBuilder.SetCreationDetails(creationUserId);
            Assert.Throws<InvalidPublicDocumentBuild>(() => documentBuilder.GetDocument());
        }
        [Fact]
        public void PublicDocumentBuilder_OnlyEligibleForReadSet_ShouldThrowInvalidPublicDocumentBuild()
        {
            documentBuilder.SetEligibleForRead(eligibleForRead);
            Assert.Throws<InvalidPublicDocumentBuild>(() => documentBuilder.GetDocument());
        }
        [Fact]
        public void PublicDocumentBuilder_OnlyEligibleForWriteSet_ShouldThrowInvalidPublicDocumentBuild()
        {
            documentBuilder.SetEligibleForWrite(eligibleForWrite);
            Assert.Throws<InvalidPublicDocumentBuild>(() => documentBuilder.GetDocument());
        }
        [Fact]
        public void PublicDocumentBuilder_ValidBuildOnlyMandatoryProperties_PropertiesShouldEqual()
        {
            documentBuilder.SetId(id);
            documentBuilder.SetTitle(title);
            documentBuilder.SetContent(content);
            documentBuilder.SetCreationDetails(creationUserId);
            documentBuilder.SetEligibleForRead(eligibleForRead);
            documentBuilder.SetEligibleForWrite(eligibleForWrite);
            PublicDocument document = documentBuilder.GetDocument();
            Assert.True(document.Id.Equals(id)
                && document._title.Equals(title)
                && document._content.Equals(content)
                && document._creationDetails.Item2.Equals(creationUserId)
                && document._creationDetails.Item1.Value.Date.Equals(DateTime.UtcNow.Date)
                && document._eligibleForWrite.Equals(eligibleForWrite)
                && document._eligibleForRead.Equals(eligibleForRead)
                && document._previousVersion is null
                && document._attachmentFilePaths is null);
        }
        [Fact]
        public void PublicDocumentBuilder_ValidBuildPreviousVersionSetted_PropertiesShouldEqual()
        {
            documentBuilder.SetId(id);
            documentBuilder.SetTitle(title);
            documentBuilder.SetContent(content);
            documentBuilder.SetCreationDetails(creationUserId);
            documentBuilder.SetEligibleForRead(eligibleForRead);
            documentBuilder.SetEligibleForWrite(eligibleForWrite);
            documentBuilder.SetPreviousVersion(previousVersion);
            PublicDocument document = documentBuilder.GetDocument();
            Assert.True(document.Id.Equals(id)
                && document._title.Equals(title)
                && document._content.Equals(content)
                && document._creationDetails.Item2.Equals(creationUserId)
                && document._creationDetails.Item1.Value.Date.Equals(DateTime.UtcNow.Date)
                && document._eligibleForWrite.Equals(eligibleForWrite)
                && document._eligibleForRead.Equals(eligibleForRead)
                && document._previousVersion.Equals(previousVersion)
                && document._attachmentFilePaths is null);
        }
        [Fact]
        public void PublicDocumentBuilder_ValidBuildAttachmentsSetted_PropertiesShouldEqual()
        {
            documentBuilder.SetId(id);
            documentBuilder.SetTitle(title);
            documentBuilder.SetContent(content);
            documentBuilder.SetCreationDetails(creationUserId);
            documentBuilder.SetEligibleForRead(eligibleForRead);
            documentBuilder.SetEligibleForWrite(eligibleForWrite);
            documentBuilder.SetAttachments(attachmentFilePaths);
            PublicDocument document = documentBuilder.GetDocument();
            Assert.True(document.Id.Equals(id)
                && document._title.Equals(title)
                && document._content.Equals(content)
                && document._creationDetails.Item2.Equals(creationUserId)
                && document._creationDetails.Item1.Value.Date.Equals(DateTime.UtcNow.Date)
                && document._eligibleForWrite.Equals(eligibleForWrite)
                && document._eligibleForRead.Equals(eligibleForRead)
                && document._previousVersion is null
                && document._attachmentFilePaths.Equals(attachmentFilePaths));
        }
        [Fact]
        public void PublicDocumentBuilder_ValidBuildAllPropertiesSetted_PropertiesShouldEqual()
        {
            documentBuilder.SetId(id);
            documentBuilder.SetTitle(title);
            documentBuilder.SetContent(content);
            documentBuilder.SetCreationDetails(creationUserId);
            documentBuilder.SetEligibleForRead(eligibleForRead);
            documentBuilder.SetEligibleForWrite(eligibleForWrite);
            documentBuilder.SetAttachments(attachmentFilePaths);
            documentBuilder.SetPreviousVersion(previousVersion);
            PublicDocument document = documentBuilder.GetDocument();
            Assert.True(document.Id.Equals(id)
                && document._title.Equals(title)
                && document._content.Equals(content)
                && document._creationDetails.Item2.Equals(creationUserId)
                && document._creationDetails.Item1.Value.Date.Equals(DateTime.UtcNow.Date)
                && document._eligibleForWrite.Equals(eligibleForWrite)
                && document._eligibleForRead.Equals(eligibleForRead)
                && document._previousVersion.Equals(previousVersion)
                && document._attachmentFilePaths.Equals(attachmentFilePaths));
        }
        [Fact]
        public void PublicDocumentBuilder_InvalidBuildIdMissing_ShouldThrowInvalidPublicDocumentBuild()
        {
            documentBuilder.SetTitle(title);
            documentBuilder.SetContent(content);
            documentBuilder.SetCreationDetails(creationUserId);
            documentBuilder.SetEligibleForRead(eligibleForRead);
            documentBuilder.SetEligibleForWrite(eligibleForWrite);
            documentBuilder.SetAttachments(attachmentFilePaths);
            documentBuilder.SetPreviousVersion(previousVersion);
            Assert.Throws<InvalidPublicDocumentBuild>(() => documentBuilder.GetDocument());
        }
        [Fact]
        public void PublicDocumentBuilder_InvalidBuildTitleMissing_ShouldThrowInvalidPublicDocumentBuild()
        {
            documentBuilder.SetId(id);
            documentBuilder.SetContent(content);
            documentBuilder.SetCreationDetails(creationUserId);
            documentBuilder.SetEligibleForRead(eligibleForRead);
            documentBuilder.SetEligibleForWrite(eligibleForWrite);
            documentBuilder.SetAttachments(attachmentFilePaths);
            documentBuilder.SetPreviousVersion(previousVersion);
            Assert.Throws<InvalidPublicDocumentBuild>(() => documentBuilder.GetDocument());
        }
        [Fact]
        public void PublicDocumentBuilder_InvalidBuildContentMissing_ShouldThrowInvalidPublicDocumentBuild()
        {
            documentBuilder.SetId(id);
            documentBuilder.SetTitle(title);
            documentBuilder.SetCreationDetails(creationUserId);
            documentBuilder.SetEligibleForRead(eligibleForRead);
            documentBuilder.SetEligibleForWrite(eligibleForWrite);
            documentBuilder.SetAttachments(attachmentFilePaths);
            documentBuilder.SetPreviousVersion(previousVersion);
            Assert.Throws<InvalidPublicDocumentBuild>(() => documentBuilder.GetDocument());
        }
        [Fact]
        public void PublicDocumentBuilder_InvalidBuildCreationDetailsMissing_ShouldThrowInvalidPublicDocumentBuild()
        {
            documentBuilder.SetId(id);
            documentBuilder.SetTitle(title);
            documentBuilder.SetContent(content);
            documentBuilder.SetEligibleForRead(eligibleForRead);
            documentBuilder.SetEligibleForWrite(eligibleForWrite);
            documentBuilder.SetAttachments(attachmentFilePaths);
            documentBuilder.SetPreviousVersion(previousVersion);
            Assert.Throws<InvalidPublicDocumentBuild>(() => documentBuilder.GetDocument());
        }
        [Fact]
        public void PublicDocumentBuilder_InvalidBuildEligibleForReadMissing_ShouldThrowInvalidPublicDocumentBuild()
        {
            documentBuilder.SetId(id);
            documentBuilder.SetTitle(title);
            documentBuilder.SetContent(content);
            documentBuilder.SetCreationDetails(creationUserId);
            documentBuilder.SetEligibleForWrite(eligibleForWrite);
            documentBuilder.SetAttachments(attachmentFilePaths);
            documentBuilder.SetPreviousVersion(previousVersion);
            Assert.Throws<InvalidPublicDocumentBuild>(() => documentBuilder.GetDocument());
        }
        [Fact]
        public void PublicDocumentBuilder_InvalidBuildEligibleForWriteMissing_ShouldThrowInvalidPublicDocumentBuild()
        {
            documentBuilder.SetId(id);
            documentBuilder.SetTitle(title);
            documentBuilder.SetContent(content);
            documentBuilder.SetCreationDetails(creationUserId);
            documentBuilder.SetEligibleForRead(eligibleForRead);
            documentBuilder.SetAttachments(attachmentFilePaths);
            documentBuilder.SetPreviousVersion(previousVersion);
            Assert.Throws<InvalidPublicDocumentBuild>(() => documentBuilder.GetDocument());
        }
        #endregion

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
        public void SetPreviousVersion_ShouldRaiseDocumentPreviousVersionSetted()
        {
            PublicDocument publicDocument = BuildValidPublicDocument();
            PublicDocument previousVersion = BuildValidPublicDocument();

            publicDocument.SetPreviousVersion(previousVersion);

            var Event = publicDocument.Events.OfType<DocumentPreviousVersionSetted>().Single();
        }
        [Fact]
        public void SetPreviousVersion_ShouldRaiseDocumentPreviousVersionSetted_PublicDocumentShouldEqual()
        {
            PublicDocument publicDocument = BuildValidPublicDocument();
            PublicDocument previousVersion = BuildValidPublicDocument();

            publicDocument.SetPreviousVersion(previousVersion);

            var Event = publicDocument.Events.OfType<DocumentPreviousVersionSetted>().Single();
            Assert.Equal(publicDocument, Event.document);
        }
        [Fact]
        public void SetPreviousVersion_ShouldRaiseDocumentPreviousVersionSetted_PreviousVersionShouldEqual()
        {
            PublicDocument publicDocument = BuildValidPublicDocument();
            PublicDocument previousVersion = BuildValidPublicDocument();

            publicDocument.SetPreviousVersion(previousVersion);

            var Event = publicDocument.Events.OfType<DocumentPreviousVersionSetted>().Single();
            Assert.Equal(previousVersion, Event.previousVersion);
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
        public void AddNewAttachmentsRange_ShouldRaiseNewAttachmentAddedForEachAttachment()
        {
            var publicDocument = BuildValidPublicDocument();
            var attachments = new List<DocumentFilePath>
            {
                new DocumentFilePath(@"C:\file.txt"),
                new DocumentFilePath(@"C:\fil.txt"),
                new DocumentFilePath(@"C:\fi.txt")
            };

            publicDocument.AddNewAttachmentsRange(attachments);

            var Events = publicDocument.Events.OfType<NewAttachmentAdded>().ToList();
            Assert.Equal(attachments.Count, Events.Count);

            foreach (var attachment in attachments)
            {
                Assert.Contains(Events, e => e.filePath == attachment && e.document == publicDocument);
            }
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

            Assert.Equal(DateTime.UtcNow.Date, publicDocument._creationDetails.Item1.Value.Date);
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
        #endregion

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
        public void SetPreviousVersion_ShouldSetPreviousVersion()
        {
            PublicDocument previousVersion = new PublicDocument();

            _publicDocument.SetPreviousVersion(previousVersion);

            Assert.Equal(previousVersion, _publicDocument._previousVersion);
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
            var initialAttachmentsCount = _publicDocument._attachmentFilePaths?.Count ?? 0;
            var attachments = new List<DocumentFilePath>
            {
                new DocumentFilePath(@"C:\file.txt"),
                new DocumentFilePath(@"C:\fil.txt"),
                new DocumentFilePath(@"C:\fi.txt")
            };

            _publicDocument.AddNewAttachmentsRange(attachments);

            Assert.NotNull(_publicDocument._attachmentFilePaths);
            foreach (var attachmentFilePath in attachmentFilePaths)
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
            Assert.Equal(userId, creationDetails.Item2);
            Assert.Equal(DateTime.UtcNow, creationDetails.Item1, TimeSpan.FromSeconds(1)); // Allowing 1 second tolerance
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
        #endregion
    }
}
