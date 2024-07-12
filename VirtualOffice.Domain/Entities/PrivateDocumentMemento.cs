using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Domain.Entities
{
    public class PrivateDocumentMemento
    {
        public Guid Id { get; }
        public string _title { get; }
        public string _content { get; }
        public ICollection<DocumentFilePath>? _attachmentFilePaths { get; }

        public DocumentCreationDate _creationDate;

        public PrivateDocumentMemento(Guid id, string title, string content, AbstractDocument? previousVersion, ICollection<DocumentFilePath>? attachmentFilePaths, DocumentCreationDate documentCreationDate)
        {
            Id = id;
            _title = title;
            _content = content;
            _attachmentFilePaths = attachmentFilePaths?.ToList();
            _creationDate = documentCreationDate;
        }
    }
}
