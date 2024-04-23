using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Builders.Document;
using VirtualOffice.Domain.Consts;
using VirtualOffice.Domain.Entities;
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
        string creationUserId = "user123";
        List<DocumentFilePath> attachmentFilePaths = new List<DocumentFilePath> { new DocumentFilePath("path/to/file") };
        List<AbstractDocument> previousVersion = new List<AbstractDocument> { new PublicDocument() };
        List<ApplicationUserId> eligibleForRead = new List<ApplicationUserId> { Guid.NewGuid()};
        List<ApplicationUserId> eligibleForWrite = new List<ApplicationUserId> { Guid.NewGuid() };

        [Fact]
        public void BuilderEmptyId()
        {
            
        }
    }
}
