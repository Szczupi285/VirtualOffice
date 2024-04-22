using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Domain.Builders
{
    public class PublicDocumentBuilder : IDocumentBuilder
    {
        public IDocumentBuilder SetAttachments(ICollection<DocumentFilePath> attachmentFilePaths)
        {
            throw new NotImplementedException();
        }

        public IDocumentBuilder SetContent(string content)
        {
            throw new NotImplementedException();
        }

        public IDocumentBuilder SetId(Guid id)
        {
            throw new NotImplementedException();
        }

        public IDocumentBuilder SetPreviousVersion(ICollection<AbstractDocument> previousVersion)
        {
            throw new NotImplementedException();
        }

        public IDocumentBuilder SetTitle(string title)
        {
            throw new NotImplementedException();
        }

        public IDocumentBuilder SetCreationDetails(ApplicationUserId id)
        {
            throw new NotImplementedException();
        }

        public IDocumentBuilder SetEligibleForRead(ICollection<ApplicationUserId> eligibleForRead)
        {
            throw new NotImplementedException();
        }
        public IDocumentBuilder SetEligibleForWrite(ICollection<ApplicationUserId> eligibleForWrite)
        {
            throw new NotImplementedException();
        }
    }
}
