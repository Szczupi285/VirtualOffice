using Microsoft.Extensions.Options;
using MongoDB.Driver;
using VirtualOffice.Application.Interfaces;
using VirtualOffice.Application.Models.ReadDatabaseSettings;

namespace VirtualOffice.Infrastructure.abstractions
{
    public abstract class AbstractModelService<T> where T : class, EntityId
    {
        protected readonly IMongoCollection<T> _Collection;

        public AbstractModelService(
            IOptions<ReadDatabaseSettings> ReadDatabaseSettings,
            string collectionName)
        {
            var mongoClient = new MongoClient(
                ReadDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                ReadDatabaseSettings.Value.DatabaseName);

            _Collection = mongoDatabase.GetCollection<T>(collectionName);
        }

        public async Task<List<T>> GetAllAsync() =>
            await _Collection.Find(_ => true).ToListAsync();

        public async Task<T?> GetAsync(string id) =>
            await _Collection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(T newUserReadModel) =>
            await _Collection.InsertOneAsync(newUserReadModel);

        public async Task UpdateAsync(string id, T updatedUserReadModel) =>
            await _Collection.ReplaceOneAsync(x => x.Id == id, updatedUserReadModel);

        public async Task RemoveAsync(string id) =>
            await _Collection.DeleteOneAsync(x => x.Id == id);
    }
}