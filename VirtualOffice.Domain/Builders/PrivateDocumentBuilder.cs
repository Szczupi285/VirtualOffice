using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Domain.Builders
{
    public class PrivateDocumentBuilder : IDocumentBuilder
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

        public PrivateDocumentBuilder SetCreationDate(DateTime creationDate)
        {
            throw new NotImplementedException();
        }
    }
}
