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

        public async Task AddAssignedEmployeesAsync(string id, List<EmployeeReadModel> employeeReadModels)
        {
            var filter = Builders<EmployeeTaskReadModel>.Filter.Eq(x => x.Id, id);

            var update = Builders<EmployeeTaskReadModel>.Update
                .AddToSetEach(x => x.AssignedEmployees, employeeReadModels);

            await _Collection.UpdateOneAsync(filter, update);
        }

        public async Task RemoveAssignedEmployeesAsync(string id, List<EmployeeReadModel> employeeReadModels)
        {
            var filter = Builders<EmployeeTaskReadModel>.Filter.Eq(x => x.Id, id);

            var update = Builders<EmployeeTaskReadModel>.Update
                .PullAll(x => x.AssignedEmployees, employeeReadModels);

            await _Collection.UpdateOneAsync(filter, update);
        }

        public async Task UpdateTitleAsync(string id, string title)
        {
            var filter = Builders<EmployeeTaskReadModel>.Filter.Eq(x => x.Id, id);

            var update = Builders<EmployeeTaskReadModel>.Update
                .Set(x => x.Title, title);

            await _Collection.UpdateOneAsync(filter, update);
        }

        public async Task UpdateDescriptionAsync(string id, string description)
        {
            var filter = Builders<EmployeeTaskReadModel>.Filter.Eq(x => x.Id, id);

            var update = Builders<EmployeeTaskReadModel>.Update
                .Set(x => x.Description, description);

            await _Collection.UpdateOneAsync(filter, update);
        }

        public async Task UpdateScheduleAsync(string id, DateTime startDate, DateTime endDate)
        {
            var filter = Builders<EmployeeTaskReadModel>.Filter.Eq(x => x.Id, id);

            var update = Builders<EmployeeTaskReadModel>.Update
                .Set(x => x.StartDate, startDate)
                .Set(x => x.EndDate, endDate);

            await _Collection.UpdateOneAsync(filter, update);
        }
    }
}