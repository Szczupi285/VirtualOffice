using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.DomainEvents.PublicDocumentEvents;
using VirtualOffice.Domain.Exceptions.Document;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Domain.Entities
{
    public class PublicDocument : AbstractDocument
    {
        //created with builder

        //public ValueTuple<DocumentCreationDate, ApplicationUserId> _creationDetails { get; private set; }

        public DocumentCreationDetails _creationDetails { get; private set; }

        public ICollection<ApplicationUserId> _eligibleForRead { get; private set; }

        public ICollection<ApplicationUserId> _eligibleForWrite { get; private set; }

        internal void AddCreationDate(ApplicationUserId applicationUserId) => _creationDetails = new(DateTime.UtcNow, applicationUserId);

        // at least one user must be eligible for read while creating the document
        internal void AddEligibleForRead(ICollection<ApplicationUserId> eligibleForRead)
            => _eligibleForRead = eligibleForRead.Count >= 1 ? eligibleForRead : throw new InvalidEligibleForReadException();

        // at least one user must be eligible for write while creating the document
        internal void AddEligibleForWrite(ICollection<ApplicationUserId> eligibleForWrite)
            => _eligibleForWrite = eligibleForWrite.Count >= 1 ? eligibleForWrite : throw new InvalidEligibleForWriteException();

        public void SettedCreationDate(ApplicationUserId userId)
        {
            AddCreationDate(userId);
            AddEvent(new PublicDocumentSettedCreationDate(this, userId, DateTime.UtcNow));
        }

        public void AddEligibleForRead(ApplicationUserId eligibleForRead)
        {
            _eligibleForRead.Add(eligibleForRead);
            AddEvent(new PublicDocumentAddedEligibleForRead(this, eligibleForRead));
        }

        public void AddEligibleForWrite(ApplicationUserId eligibleForWrite)
        {
            _eligibleForWrite.Add(eligibleForWrite);
            AddEvent(new PublicDocumentAddedEligibleForWrite(this, eligibleForWrite));
        }

        public PublicDocumentMemento SaveToMemento()
        {
            return new PublicDocumentMemento(Id, _title, _content, _attachmentFilePaths, _creationDetails, _eligibleForRead, _eligibleForWrite);
        }

        public void RestoreFromMemento(PublicDocumentMemento memento)
        {
            // properies are not valdiated by add methods since they were already validated when the object was constructed and saved to memento

            Id = memento.Id;
            _title = memento._title;
            _content = memento._content;
            _attachmentFilePaths = memento._attachmentFilePaths;
            _creationDetails = memento._creationDetails;
            _eligibleForRead = memento._eligibleForRead;
            _eligibleForWrite = memento._eligibleForWrite;
        }
    }
}