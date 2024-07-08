using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Domain.Repositories
{
    public interface IPublicDocumentRepository
    {
        PublicDocument GetById(DocumentId guid);
        PublicDocument GetPreviousVersion(DocumentId guid);
        void Add(PublicDocument PublicDocument);
        void Update(PublicDocument PublicDocument);
        void Delete(DocumentId guid);
        IEnumerable<PublicDocument> GetAllForUser(ApplicationUserId userId);
        IEnumerable<PublicDocument> GetAllSortedForUser(ApplicationUserId userId);
        IEnumerable<PublicDocument> GetAllForUserByDate(ApplicationUserId userId, DocumentCreationDate date);
        Task SaveAsync(CancellationToken cancellationToken);
    }
}
