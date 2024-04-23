using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Domain.Builders.Document
{
    internal class PrivateDocumentBuilder : IDocumentBuilder
    {
        private PrivateDocument _document = new PrivateDocument();

        public PrivateDocumentBuilder()
        {
            Reset();
        }
        public void Reset() => _document = new PrivateDocument();

        public void SetAttachments(ICollection<DocumentFilePath> attachmentFilePaths)
        {
            _document.AddAttachment(attachmentFilePaths);
        }

        public void SetContent(string content)
        {
            _document.AddContent(content);
        }

        public void SetId(Guid id)
        {
            _document.AddId(id);
        }

        public void SetPreviousVersion(AbstractDocument previousVersion)
        {
            _document.AddPreviousVersion(previousVersion);
        }

        public void SetTitle(string title)
        {
            _document.AddTitle(title);
        }

        public void SetCreationDate(DateTime creationDate)
        {
            _document.AddCreationDate(creationDate);
        }

        public PrivateDocument GetDocument()
        {
            PrivateDocument document = _document;
            Reset();

            return document;
        }
    }
}
