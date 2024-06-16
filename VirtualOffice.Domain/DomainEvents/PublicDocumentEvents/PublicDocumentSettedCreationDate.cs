using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Domain.DomainEvents.PublicDocumentEvents
{
    public record PublicDocumentSettedCreationDate(PublicDocument document, ApplicationUserId userId, DocumentCreationDate date) : IDomainEvent;
    
}
