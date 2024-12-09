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

        public async Task AddEmployeeToOffice(string orgId, string offId, EmployeeReadModel employee)
        {
            var filter = Builders<OrganizationReadModel>.Filter.And(
                Builders<OrganizationReadModel>.Filter.Eq(x => x.Id, orgId),
                Builders<OrganizationReadModel>.Filter.ElemMatch(
                    x => x.Offices, office => office.Id == offId));

            var update = Builders<OrganizationReadModel>.Update.Push(
                x => x.Offices[-1].Employees, employee);

            // Step 3: Perform the update operation
            var result = await _Collection.UpdateOneAsync(filter, update);
        }
    }
}