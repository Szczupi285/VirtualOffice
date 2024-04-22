using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Domain.Entities
{
    public class PrivateDocument : AbstractDocument
    {
        public DateTime _creationDate { get; private set; } = DateTime.Now;

        public PrivateDocument(Guid id, string title, string content, ICollection<AbstractDocument> previousVersions, ICollection<DocumentFilePath> documentFilePaths)
        {
            Id = id;
            _title = title;
            _content = content;
            _previousVersions = previousVersions;
            _attachmentFilePaths = documentFilePaths;
        }
        public PrivateDocument(Guid id, string title, string content, ICollection<AbstractDocument> previousVersions)
        {
            Id = id;
            _title = title;
            _content = content;
            _previousVersions = previousVersions;
        }
        public PrivateDocument(Guid id, string title, string content, ICollection<DocumentFilePath> documentFilePaths)
        {
            Id = id;
            _title = title;
            _content = content;
            _attachmentFilePaths = documentFilePaths;
        }
        public PrivateDocument(Guid id, string title, string content)
        {
            Id = id;
            _title = title;
            _content = content;
        }
    }
    
}
