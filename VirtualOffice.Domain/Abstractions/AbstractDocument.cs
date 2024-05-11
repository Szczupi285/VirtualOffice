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

        public AbstractDocument? _previousVersion { get; private protected set; }

        public ICollection<DocumentFilePath>? _attachmentFilePaths { get; private protected set; }

        internal void AddId(Guid id) => Id = id;

        internal void AddTitle(string title) => _title = title;

        internal void AddContent(string content) => _content = content;

        internal void AddPreviousVersion(AbstractDocument previousVersion) => _previousVersion = previousVersion;

        internal void AddAttachment(ICollection<DocumentFilePath> attachmentFilePaths) => _attachmentFilePaths = attachmentFilePaths; 



        
    }
}
