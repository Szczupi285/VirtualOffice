using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Domain.Abstractions
{
    public abstract class AbstractDocument
    {
        public DocumentId Id { get; private set; }
        public DocumentTitle _title { get; private set; }

        public DocumentContent _content { get; private set; }

        public ICollection<AbstractDocument> _previousVersions { get; private set; }

        public ICollection<DocumentFilePath> _attachmentFilePaths { get; private set; }

    }
}
