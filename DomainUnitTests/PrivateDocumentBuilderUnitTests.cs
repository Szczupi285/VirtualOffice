using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Builders.Document;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.Exceptions.BuilderExceptions;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.Document;

namespace DomainUnitTests
{
    public class PrivateDocumentBuilderUnitTests
    {
        PrivateDocumentBuilder documentBuilder = new PrivateDocumentBuilder();
        Guid id = Guid.NewGuid();
        string content = "Sample content";
        string title = "Sample title";
        List<DocumentFilePath> attachmentFilePaths = new List<DocumentFilePath> { new DocumentFilePath("path/to/file") };
        AbstractDocument previousVersion = new PrivateDocument();

        [Fact]
        public void PrivateDocumentBuilder_NoPropertiesSet_ShouldThrowInvalidPrivateDocumentBuild()
        {
            Assert.Throws<InvalidPrivateDocumentBuild>(() => documentBuilder.GetDocument());
        }
        [Fact]
        public void PrivateDocumentBuilder_OnlyIdSet_ShouldThrowInvalidPrivateDocumentBuild()
        {
            documentBuilder.SetId(id);
            Assert.Throws<InvalidPrivateDocumentBuild>(() => documentBuilder.GetDocument());
        }
        [Fact]
        public void PrivateDocumentBuilder_OnlyContentSet_ShouldThrowInvalidPrivateDocumentBuild()
        {
            documentBuilder.SetContent(content);
            Assert.Throws<InvalidPrivateDocumentBuild>(() => documentBuilder.GetDocument());
        }
        [Fact]
        public void PrivateDocumentBuilder_OnlyTitleSet_ShouldThrowInvalidPrivateDocumentBuild()
        {
            documentBuilder.SetTitle(title);
            Assert.Throws<InvalidPrivateDocumentBuild>(() => documentBuilder.GetDocument());
        }
      
        [Fact]
        public void PrivateDocumentBuilder_ValidBuildOnlyMandatoryProperties_PropertiesShouldEqual()
        {
            documentBuilder.SetId(id);
            documentBuilder.SetTitle(title);
            documentBuilder.SetContent(content);
            PrivateDocument document = documentBuilder.GetDocument();
            Assert.True(document.Id.Equals(id)
                && document._title.Equals(title)
                && document._content.Equals(content)
                && document._previousVersion is null
                && document._attachmentFilePaths is null);
        }
        [Fact]
        public void PrivateDocumentBuilder_ValidBuildPreviousVersionSetted_PropertiesShouldEqual()
        {
            documentBuilder.SetId(id);
            documentBuilder.SetTitle(title);
            documentBuilder.SetContent(content);
            documentBuilder.SetPreviousVersion(previousVersion);
            PrivateDocument document = documentBuilder.GetDocument();
            Assert.True(document.Id.Equals(id)
                && document._title.Equals(title)
                && document._content.Equals(content)
                && document._previousVersion.Equals(previousVersion)
                && document._attachmentFilePaths is null);
        }
        [Fact]
        public void PrivateDocumentBuilder_ValidBuildAttachmentsSetted_PropertiesShouldEqual()
        {
            documentBuilder.SetId(id);
            documentBuilder.SetTitle(title);
            documentBuilder.SetContent(content);
            documentBuilder.SetAttachments(attachmentFilePaths);
            PrivateDocument document = documentBuilder.GetDocument();
            Assert.True(document.Id.Equals(id)
                && document._title.Equals(title)
                && document._content.Equals(content)
                && document._previousVersion is null
                && document._attachmentFilePaths.Equals(attachmentFilePaths));
        }
        [Fact]
        public void PrivateDocumentBuilder_ValidBuildAllPropertiesSetted_PropertiesShouldEqual()
        {
            documentBuilder.SetId(id);
            documentBuilder.SetTitle(title);
            documentBuilder.SetContent(content);
            documentBuilder.SetAttachments(attachmentFilePaths);
            documentBuilder.SetPreviousVersion(previousVersion);
            PrivateDocument document = documentBuilder.GetDocument();
            Assert.True(document.Id.Equals(id)
                && document._title.Equals(title)
                && document._content.Equals(content)
                && document._previousVersion.Equals(previousVersion)
                && document._attachmentFilePaths.Equals(attachmentFilePaths));
        }

        [Fact]
        public void PrivateDocumentBuilder_InvalidBuildIdMissing_ShouldThrowInvalidPrivateDocumentBuild()
        {
            documentBuilder.SetTitle(title);
            documentBuilder.SetContent(content);
            documentBuilder.SetAttachments(attachmentFilePaths);
            documentBuilder.SetPreviousVersion(previousVersion);
            Assert.Throws<InvalidPrivateDocumentBuild>(() => documentBuilder.GetDocument());
        }

        [Fact]
        public void PrivateDocumentBuilder_InvalidBuildTitleMissing_ShouldThrowInvalidPrivateDocumentBuild()
        {
            documentBuilder.SetId(id);
            documentBuilder.SetContent(content);
            documentBuilder.SetAttachments(attachmentFilePaths);
            documentBuilder.SetPreviousVersion(previousVersion);
            Assert.Throws<InvalidPrivateDocumentBuild>(() => documentBuilder.GetDocument());
        }

        [Fact]
        public void PrivateDocumentBuilder_InvalidBuildContentMissing_ShouldThrowInvalidPrivateDocumentBuild()
        {
            documentBuilder.SetId(id);
            documentBuilder.SetTitle(title);
            documentBuilder.SetAttachments(attachmentFilePaths);
            documentBuilder.SetPreviousVersion(previousVersion);
            Assert.Throws<InvalidPrivateDocumentBuild>(() => documentBuilder.GetDocument());
        }
    }
}
