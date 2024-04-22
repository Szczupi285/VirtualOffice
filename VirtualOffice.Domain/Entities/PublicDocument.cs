using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Abstractions;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace VirtualOffice.Domain.Entities
{
    public class PublicDocument : AbstractDocument
    {
        public ValueTuple<DateTime, ApplicationUserId> _creationDateAndBy { get; private set; }

        public ICollection<ApplicationUser> _eligibleForRead {  get; private set; }

        public ICollection<ApplicationUser> _eligibleForWrite {  get; private set; }
    }
}
