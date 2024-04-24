﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Builders.Document;
using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.BuilderExceptions;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.Document;

namespace DomainUnitTests
{
    public class PublicDocumentbuilderUnitTests
    {
        PublicDocumentBuilder documentBuilder = new PublicDocumentBuilder();
        Guid id = Guid.NewGuid();
        string content = "Sample content";
        string title = "Sample title";
        ApplicationUserId creationUserId = Guid.NewGuid();
        List<ApplicationUserId> eligibleForRead = new List<ApplicationUserId> { Guid.NewGuid()};
        List<ApplicationUserId> eligibleForWrite = new List<ApplicationUserId> { Guid.NewGuid() };
        List<DocumentFilePath> attachmentFilePaths = new List<DocumentFilePath> { new DocumentFilePath("path/to/file") };
        AbstractDocument previousVersion = new PublicDocument();

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
                && document._creationDetails.Item1.Date.Equals(DateTime.Now.Date)
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
                && document._creationDetails.Item1.Date.Equals(DateTime.Now.Date)
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
                && document._creationDetails.Item1.Date.Equals(DateTime.Now.Date)
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
                && document._creationDetails.Item1.Date.Equals(DateTime.Now.Date)
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
       

    }
}
