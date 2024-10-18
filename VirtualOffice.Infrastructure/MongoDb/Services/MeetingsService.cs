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
    }
}