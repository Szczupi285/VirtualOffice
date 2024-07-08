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
        PrivateDocument GetById(DocumentId guid);
        PrivateDocument GetPreviousVersion(DocumentId guid);
        void Add(PrivateDocument PrivateDocument);
        void Update(PrivateDocument PrivateDocument);
        void Delete(DocumentId guid);
        IEnumerable<PrivateDocument> GetAllForUser(ApplicationUser userId);
        IEnumerable<PrivateDocument> GetAllSortedForUser(ApplicationUser userId);
        IEnumerable<PublicDocument> GetAllForUserByDate(ApplicationUserId userId, DocumentCreationDate date);
        Task SaveAsync(CancellationToken cancellationToken);
    }
}
