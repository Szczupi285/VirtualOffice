using Microsoft.Extensions.Options;
using MongoDB.Driver;
using VirtualOffice.Application.Models;
using VirtualOffice.Application.Models.ReadDatabaseSettings;
using VirtualOffice.Infrastructure.abstractions;

namespace VirtualOffice.Infrastructure.MongoDb.Services
{
    public class EmployeeTasksService : AbstractModelService<EmployeeTaskReadModel>
    {
        public EmployeeTasksService(IOptions<ReadDatabaseSettings> ReadDatabaseSettings)
            : base(ReadDatabaseSettings, ReadDatabaseSettings.Value.EmployeeTasksCollectionName)
        {
        }

        public async Task UpdateEmployeeTaskAssignedEmployees(string id, List<EmployeeReadModel> employeeReadModels)
        {
            var filter = Builders<EmployeeTaskReadModel>.Filter.Eq(x => x.Id, id);

            var update = Builders<EmployeeTaskReadModel>.Update
                .AddToSetEach(x => x.AssignedEmployees, employeeReadModels);

            await _Collection.UpdateOneAsync(filter, update);
        }
    }
}