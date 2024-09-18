using VirtualOffice.Domain.DomainEvents.AbstractDocumentEvents;
using VirtualOffice.Domain.Exceptions.Document;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Domain.Abstractions
{
    public abstract class AbstractDocument : AggregateRoot<DocumentId>
    {
        // created with builder.

        public DocumentTitle _title { get; private protected set; }

        public DocumentContent _content { get; private protected set; }

        public ICollection<DocumentFilePath>? _attachmentFilePaths { get; private protected set; }

        internal void AddId(Guid id) => Id = id;

        internal void AddTitle(string title) => _title = title;

        internal void AddContent(string content) => _content = content;

        internal void AddAttachment(ICollection<DocumentFilePath> attachmentFilePaths) => _attachmentFilePaths = attachmentFilePaths;

        public void SetTitle(DocumentTitle title)
        {
            _title = title;
            AddEvent(new DocumentTitleSetted(this, title));
        }

        public void SetContent(DocumentContent content)
        {
            _content = content;
            AddEvent(new DocumentContentSetted(this, content));
        }

        public void AddNewAttachment(DocumentFilePath attachmentFilePath)
        {
            if (_attachmentFilePaths is not null)
            {
                if (!_attachmentFilePaths.Contains(attachmentFilePath))
                    _attachmentFilePaths.Add(attachmentFilePath);
            }
            else
                _attachmentFilePaths = new List<DocumentFilePath>() { attachmentFilePath };

            AddEvent(new NewAttachmentAdded(this, attachmentFilePath));
        }

        public void AddNewAttachmentsRange(ICollection<DocumentFilePath> documentFilePaths)
        {
            foreach (DocumentFilePath documentFilePath in documentFilePaths)
            {
                AddNewAttachment(documentFilePath);
            }
        }

        public void DeleteAttachment(DocumentFilePath attachmentFilePath)
        {
            if (_attachmentFilePaths != null)
            {
                if (!_attachmentFilePaths.Contains(attachmentFilePath))
                {
                    throw new InvalidDocumentFilePathException(attachmentFilePath);
                }
                _attachmentFilePaths.Remove(attachmentFilePath);

                AddEvent(new AttachmentDeleted(this, attachmentFilePath));
            }
        }

        public void DeleteAttachmentsRange(ICollection<DocumentFilePath> documentFilePaths)
        {
            foreach (DocumentFilePath documentFilePath in documentFilePaths)
            {
                DeleteAttachment(documentFilePath);
            }
        }
    }
}