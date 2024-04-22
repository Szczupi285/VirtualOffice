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
        public DocumentId Id { get; private protected set; }
        public DocumentTitle _title { get; private protected set; }

        public DocumentContent _content { get; private protected set; }

        public ICollection<AbstractDocument>? _previousVersions { get; private protected set; }

        public ICollection<DocumentFilePath>? _attachmentFilePaths { get; private protected set; }




        
    }
}
