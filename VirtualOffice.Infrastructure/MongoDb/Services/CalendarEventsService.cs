using Microsoft.Extensions.Options;
using MongoDB.Driver;
using VirtualOffice.Application.Models;
using VirtualOffice.Application.Models.ReadDatabaseSettings;
using VirtualOffice.Infrastructure.abstractions;

namespace VirtualOffice.Infrastructure.MongoDb.Services
{
    public class CalendarEventsService : AbstractModelService<CalendarEventReadModel>
    {
        public CalendarEventsService(IOptions<ReadDatabaseSettings> ReadDatabaseSettings)
            : base(ReadDatabaseSettings, ReadDatabaseSettings.Value.CalendarEventsCollectionName)
        {
        }

        public async Task AddAssignedEmployeesAsync(string id, List<EmployeeReadModel> employeeReadModels)
        {
            var filter = Builders<CalendarEventReadModel>.Filter.Eq(x => x.Id, id);

            var update = Builders<CalendarEventReadModel>.Update
                .AddToSetEach(x => x.AssignedEmployees, employeeReadModels);

            await _Collection.UpdateOneAsync(filter, update);
        }

        public async Task RemoveAssignedEmployeesAsync(string id, List<EmployeeReadModel> employeeReadModels)
        {
            var filter = Builders<CalendarEventReadModel>.Filter.Eq(x => x.Id, id);

            var update = Builders<CalendarEventReadModel>.Update
                .PullAll(x => x.AssignedEmployees, employeeReadModels);

            await _Collection.UpdateOneAsync(filter, update);
        }

        public async Task UpdateTitleAsync(string id, string title)
        {
            var filter = Builders<CalendarEventReadModel>.Filter.Eq(x => x.Id, id);

            var update = Builders<CalendarEventReadModel>.Update
                .Set(x => x.Title, title);

            await _Collection.UpdateOneAsync(filter, update);
        }

        public async Task UpdateDescriptionAsync(string id, string description)
        {
            var filter = Builders<CalendarEventReadModel>.Filter.Eq(x => x.Id, id);

            var update = Builders<CalendarEventReadModel>.Update
                .Set(x => x.Description, description);

            await _Collection.UpdateOneAsync(filter, update);
        }

        public async Task UpdateScheduleAsync(string id, DateTime startDate, DateTime endDate)
        {
            var filter = Builders<CalendarEventReadModel>.Filter.Eq(x => x.Id, id);

            var startDateUtc = DateTime.SpecifyKind(startDate, DateTimeKind.Utc);
            var endDateUtc = DateTime.SpecifyKind(endDate, DateTimeKind.Utc);

            var update = Builders<CalendarEventReadModel>.Update
                .Set(x => x.StartDate, startDate)
                .Set(x => x.EndDate, endDate);

            await _Collection.UpdateOneAsync(filter, update);
        }
    }
}