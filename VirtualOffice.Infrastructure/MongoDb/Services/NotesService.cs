using Microsoft.Extensions.Options;
using MongoDB.Driver;
using VirtualOffice.Application.Models;
using VirtualOffice.Application.Models.ReadDatabaseSettings;
using VirtualOffice.Infrastructure.abstractions;

namespace VirtualOffice.Infrastructure.MongoDb.Services
{
    public class NotesService : AbstractModelService<NoteReadModel>
    {
        public NotesService(IOptions<ReadDatabaseSettings> ReadDatabaseSettings)
            : base(ReadDatabaseSettings, ReadDatabaseSettings.Value.NotesCollectionName)
        { }

        public async Task UpdateTitleAsync(string id, string title)
        {
            var filter = Builders<NoteReadModel>.Filter.Eq(x => x.Id, id);

            var update = Builders<NoteReadModel>.Update
                .Set(x => x.Title, title);

            await _Collection.UpdateOneAsync(filter, update);
        }
    }
}