using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.Document;
using VirtualOffice.Domain.ValueObjects.Note;

namespace VirtualOffice.Domain.Repositories
{
    public interface IPrivateDocumentRepository
    {
        Task<PrivateDocument> GetById(DocumentId guid);
        Task<PrivateDocument> GetPreviousVersion(DocumentId guid);
        Task Add(PrivateDocument PrivateDocument);
        Task Update(PrivateDocument PrivateDocument);
        Task Delete(DocumentId guid);
        Task<IEnumerable<PrivateDocument>> GetAllForUser(ApplicationUser userId);
        Task<IEnumerable<PrivateDocument>> GetAllSortedForUser(ApplicationUser userId);
        Task<IEnumerable<PublicDocument>> GetAllForUserByDate(ApplicationUserId userId, DocumentCreationDate date);
        Task SaveAsync(CancellationToken cancellationToken);
    }
}
