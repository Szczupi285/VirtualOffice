using VirtualOffice.Domain.Entities;
using VirtualOffice.Domain.ValueObjects.ApplicationUser;
using VirtualOffice.Domain.ValueObjects.Document;

namespace VirtualOffice.Domain.Repositories
{
    public interface IPrivateDocumentRepository
    {
        Task<PrivateDocument> GetById(DocumentId guid);

        Task AddAsync(PrivateDocument PrivateDocument);

        Task UpdateAsync(PrivateDocument PrivateDocument);

        Task DeleteAsync(DocumentId guid);
    }
}