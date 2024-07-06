using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public ValueTuple<DocumentCreationDate, ApplicationUserId> _creationDetails { get; private set; }

        public ICollection<ApplicationUserId> _eligibleForRead {  get; private set; }

        public ICollection<ApplicationUserId> _eligibleForWrite {  get; private set; }


        internal void AddCreationDate(ApplicationUserId applicationUserId) => _creationDetails = (DateTime.UtcNow, applicationUserId);

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
    }
}
