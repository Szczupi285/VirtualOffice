using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Domain.Builders.Document
{
    internal interface IDocumentBuilder
    {
        void SetId(Guid id);
        void SetTitle(string title);
        void SetContent(string content);
        void SetPreviousVersion(AbstractDocument previousVersion);
        void SetAttachments(ICollection<DocumentFilePath> attachmentFilePaths);

    }
}
