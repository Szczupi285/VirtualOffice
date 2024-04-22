using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Domain.Builders
{
    public interface IDocumentBuilder
    {
        IDocumentBuilder SetId(Guid id);
        IDocumentBuilder SetTitle(string title);
        IDocumentBuilder SetContent(string content);
        IDocumentBuilder SetPreviousVersion(ICollection<AbstractDocument> previousVersion);
        IDocumentBuilder SetAttachments(ICollection<DocumentFilePath> attachmentFilePaths);

    }
}
