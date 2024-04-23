using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Domain.Builders
{
    internal class PublicDocumentBuilder : IDocumentBuilder
    {
        private PublicDocument _document = new PublicDocument();

        public PublicDocumentBuilder()
        {
            this.Reset();
        }

        public void Reset() => this._document = new PublicDocument();

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

        public void SetPreviousVersion(AbstractDocument previousVersion)
        {
            this._document.AddPreviousVersion(previousVersion);
        }

        public void SetTitle(string title)
        {
            this._document.AddTitle(title);
        }

        public void SetCreationDetails(ApplicationUserId id)
        {
            this._document.AddCreationDate(id);
        }

        public void SetEligibleForRead(ICollection<ApplicationUserId> eligibleForRead)
        {
            this._document.AddEligibleForRead(eligibleForRead);
        }
        public void SetEligibleForWrite(ICollection<ApplicationUserId> eligibleForWrite)
        {
            this._document.AddEligibleForWrite(eligibleForWrite);
        }

        public PublicDocument GetDocument()
        {
            PublicDocument document = _document;

            this.Reset();

            return document;    
        }
    }
}
