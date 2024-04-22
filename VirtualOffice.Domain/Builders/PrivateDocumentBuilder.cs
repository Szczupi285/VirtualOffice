using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Domain.Builders
{
    internal class PrivateDocumentBuilder : IDocumentBuilder
    {
        private PrivateDocument _document = new PrivateDocument();

        public PrivateDocumentBuilder()
        {
            this.Reset();
        }
        public void Reset() => this._document = new PrivateDocument();

        public void SetAttachments(ICollection<DocumentFilePath> attachmentFilePaths)
        {
            this._document.AddAttachment(attachmentFilePaths);
        }

        public void SetContent(string content)
        {
            this._document.AddContent(content);
        }

        public void SetId(Guid id)
        {
            this._document.AddId(id);
        }

        public void SetPreviousVersion(ICollection<AbstractDocument> previousVersion)
        {
            this._document.AddPreviousVersion(previousVersion);
        }

        public void SetTitle(string title)
        {
            this._document.AddTitle(title);
        }

        public void SetCreationDate(DateTime creationDate)
        {
            this._document.AddCreationDate(creationDate);
        }

        public PrivateDocument GetDocument()
        {
            PrivateDocument document = _document;
            this.Reset();

            return document;
        }
    }
}
