using Microsoft.Extensions.Options;
using MongoDB.Driver;
using VirtualOffice.Application.Models;
using VirtualOffice.Application.Models.ReadDatabaseSettings;
using VirtualOffice.Infrastructure.abstractions;

namespace VirtualOffice.Infrastructure.MongoDb.Services
{
    public class MeetingsService : AbstractModelService<MeetingReadModel>
    {
        public MeetingsService(IOptions<ReadDatabaseSettings> ReadDatabaseSettings)
            : base(ReadDatabaseSettings, ReadDatabaseSettings.Value.MeetingsCollectionName)
        {
        }

        public async Task AddAssignedEmployeesAsync(string id, List<EmployeeReadModel> employeeReadModels)
        {
            var filter = Builders<MeetingReadModel>.Filter.Eq(x => x.Id, id);

            var update = Builders<MeetingReadModel>.Update
                .AddToSetEach(x => x.AssignedEmployees, employeeReadModels);

            await _Collection.UpdateOneAsync(filter, update);
        }

        public async Task RemoveAssignedEmployeesAsync(string id, List<EmployeeReadModel> employeeReadModels)
        {
            var filter = Builders<MeetingReadModel>.Filter.Eq(x => x.Id, id);

            var update = Builders<MeetingReadModel>.Update
                .PullAll(x => x.AssignedEmployees, employeeReadModels);

            await _Collection.UpdateOneAsync(filter, update);
        }

        public async Task UpdateTitleAsync(string id, string title)
        {
            var filter = Builders<MeetingReadModel>.Filter.Eq(x => x.Id, id);

            var update = Builders<MeetingReadModel>.Update
                .Set(x => x.Title, title);

            await _Collection.UpdateOneAsync(filter, update);
        }

        public async Task UpdateDescriptionAsync(string id, string description)
        {
            var filter = Builders<MeetingReadModel>.Filter.Eq(x => x.Id, id);

            var update = Builders<MeetingReadModel>.Update
                .Set(x => x.Description, description);

            await _Collection.UpdateOneAsync(filter, update);
        }

        public async Task UpdateScheduleAsync(string id, DateTime startDate, DateTime endDate)
        {
            var filter = Builders<MeetingReadModel>.Filter.Eq(x => x.Id, id);

            var startDateUtc = DateTime.SpecifyKind(startDate, DateTimeKind.Utc);
            var endDateUtc = DateTime.SpecifyKind(endDate, DateTimeKind.Utc);

            var update = Builders<MeetingReadModel>.Update
                .Set(x => x.StartDate, startDate)
                .Set(x => x.EndDate, endDate);

            await _Collection.UpdateOneAsync(filter, update);
        }
    }
}