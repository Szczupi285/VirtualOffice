using Microsoft.Extensions.Options;
using MongoDB.Driver;
using VirtualOffice.Application.Models;
using VirtualOffice.Application.Models.ReadDatabaseSettings;
using VirtualOffice.Infrastructure.abstractions;

namespace VirtualOffice.Infrastructure.MongoDb.Services
{
    public class OrganizationsService : AbstractModelService<OrganizationReadModel>
    {
        public OrganizationsService(IOptions<ReadDatabaseSettings> ReadDatabaseSettings)
            : base(ReadDatabaseSettings, ReadDatabaseSettings.Value.OrganizationsCollectionName)
        { }

        public async Task AddOfficeAsync(string id, OfficeReadModel OfficeReadModel)
        {
            var filter = Builders<OrganizationReadModel>.Filter.Eq(x => x.Id, id);

            var update = Builders<OrganizationReadModel>.Update
                .AddToSetEach(x => x.Offices, new List<OfficeReadModel>() { OfficeReadModel });

            await _Collection.UpdateOneAsync(filter, update);
        }
    }
}