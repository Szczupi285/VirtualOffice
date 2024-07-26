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
        Task<PublicDocument> GetById(DocumentId guid);
        Task<PublicDocument> GetPreviousVersion(DocumentId guid);
        Task Add(PublicDocument PublicDocument);
        Task Update(PublicDocument PublicDocument);
        Task Delete(DocumentId guid);
        Task<IEnumerable<PublicDocument>> GetAllForUser(ApplicationUserId userId);
        Task<IEnumerable<PublicDocument>> GetAllSortedForUser(ApplicationUserId userId);
        Task<IEnumerable<PublicDocument>> GetAllForUserByDate(ApplicationUserId userId, DocumentCreationDate date);
        Task SaveAsync(CancellationToken cancellationToken);
    }
}
