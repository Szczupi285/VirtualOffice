using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;

namespace VirtualOffice.Domain.DomainEvents.PublicDocumentEvents
{
    public record PublicDocumentAddedEligibleForWrite(PublicDocument document, ApplicationUserId userId) : IDomainEvent;

}
