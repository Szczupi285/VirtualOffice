using Microsoft.Extensions.Options;
using VirtualOffice.Application.Models;
using VirtualOffice.Application.Models.ReadDatabaseSettings;
using VirtualOffice.Infrastructure.abstractions;

namespace VirtualOffice.Infrastructure.MongoDb.Services
{
    internal class PublicDocumentsService : AbstractModelService<PublicDocumentReadModel>
    {
        public PublicDocumentsService(IOptions<ReadDatabaseSettings> ReadDatabaseSettings)
            : base(ReadDatabaseSettings, ReadDatabaseSettings.Value.PublicDocumentsCollectionName)
        {
        }
    }
}