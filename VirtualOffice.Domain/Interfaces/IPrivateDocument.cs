using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Domain.Interfaces
{
    public interface IPrivateDocument
    {
        DocumentCreationDate _creationDate { get; }
        ICollection<DocumentFilePath>? _attachmentFilePaths { get; }
        DocumentContent _content { get; }
        DocumentTitle _title { get; }

        void AddNewAttachment(DocumentFilePath attachmentFilePath);
        void AddNewAttachmentsRange(ICollection<DocumentFilePath> documentFilePaths);
        void DeleteAttachment(DocumentFilePath attachmentFilePath);
        void DeleteAttachmentsRange(ICollection<DocumentFilePath> documentFilePaths);
        void SetContent(DocumentContent content);
        void SetTitle(DocumentTitle title);
        PrivateDocumentMemento SaveToMemento();
        void RestoreFromMemento(PrivateDocumentMemento memento)
    }
}