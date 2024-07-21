using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Domain.Interfaces
{
    public interface IPublicDocument
    {
        ValueTuple<DocumentCreationDate, ApplicationUserId> _creationDetails { get; }
        ICollection<ApplicationUserId> _eligibleForRead { get; }
        ICollection<ApplicationUserId> _eligibleForWrite { get; }
        ICollection<DocumentFilePath>? _attachmentFilePaths { get; }
        DocumentContent _content { get; }
        DocumentTitle _title { get; }

        void AddNewAttachment(DocumentFilePath attachmentFilePath);
        void AddNewAttachmentsRange(ICollection<DocumentFilePath> documentFilePaths);
        void DeleteAttachment(DocumentFilePath attachmentFilePath);
        void DeleteAttachmentsRange(ICollection<DocumentFilePath> documentFilePaths);
        void SetContent(DocumentContent content);
        void SetTitle(DocumentTitle title);
        void AddCreationDate(ApplicationUserId applicationUserId);
        void AddEligibleForRead(ICollection<ApplicationUserId> eligibleForRead);
        void AddEligibleForWrite(ICollection<ApplicationUserId> eligibleForWrite);
        void SettedCreationDate(ApplicationUserId userId);
        void AddEligibleForRead(ApplicationUserId eligibleForRead);
        void AddEligibleForWrite(ApplicationUserId eligibleForWrite);
        PublicDocumentMemento SaveToMemento();
        void RestoreFromMemento(PublicDocumentMemento memento);
    }
}