using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Domain.Entities
{
    public class PublicDocument : AbstractDocument
    {
        public ValueTuple<DateTime, ApplicationUserId> _creationDetails { get; private set; }

        public ICollection<ApplicationUserId> _eligibleForRead {  get; private set; }

        public ICollection<ApplicationUserId> _eligibleForWrite {  get; private set; }



        public void AddCreationDate(ApplicationUserId applicationUserId) => _creationDetails = (DateTime.Now, applicationUserId);
        public void AddEligibleForRead(ICollection<ApplicationUserId> eligibleForRead) => _eligibleForRead = eligibleForRead;
        public void AddEligibleForWrite(ICollection<ApplicationUserId> eligibleForWrite) => _eligibleForWrite = eligibleForWrite;


    }
}
