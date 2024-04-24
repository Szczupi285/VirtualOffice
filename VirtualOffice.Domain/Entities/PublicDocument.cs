using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.Exceptions.Document;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Domain.Entities
{
    public class PublicDocument : AbstractDocument
    {
        public ValueTuple<DateTime, ApplicationUserId> _creationDetails { get; private set; }

        public ICollection<ApplicationUserId> _eligibleForRead {  get; private set; }

        public ICollection<ApplicationUserId> _eligibleForWrite {  get; private set; }



        internal void AddCreationDate(ApplicationUserId applicationUserId) => _creationDetails = (DateTime.Now, applicationUserId);
        internal void AddEligibleForRead(ICollection<ApplicationUserId> eligibleForRead)
            => _eligibleForRead = eligibleForRead.Count >= 1 ? eligibleForRead : throw new InvalidEligibleForReadException();
        internal void AddEligibleForWrite(ICollection<ApplicationUserId> eligibleForWrite) => _eligibleForWrite = eligibleForWrite.Count >= 1 ? eligibleForWrite : throw new InvalidEligibleForWriteException();


    }
}
