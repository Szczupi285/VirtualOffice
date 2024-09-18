using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VirtualOffice.Infrastructure.EF.Models;
using VirtualOffice.Infrastructure.EF.Models.ReadDatabaseSettings;

namespace VirtualOffice.Infrastructure.MongoDb.Services
{
    public class EmployeesService
    {
        private readonly IMongoCollection<UserReadModel> _employeesCollection;

        public EmployeesService(
            IOptions<ReadDatabaseSettings> ReadDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                ReadDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                ReadDatabaseSettings.Value.DatabaseName);

            _employeesCollection = mongoDatabase.GetCollection<UserReadModel>(
                ReadDatabaseSettings.Value.EmployeesCollectionName);
        }

        public async Task<List<UserReadModel>> GetAsync() =>
            await _employeesCollection.Find(_ => true).ToListAsync();

        public async Task<UserReadModel?> GetAsync(string id) =>
            await _employeesCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(UserReadModel newUserReadModel) =>
            await _employeesCollection.InsertOneAsync(newUserReadModel);

        public async Task UpdateAsync(string id, UserReadModel updatedUserReadModel) =>
            await _employeesCollection.ReplaceOneAsync(x => x.Id == id, updatedUserReadModel);

        public async Task RemoveAsync(string id) =>
            await _employeesCollection.DeleteOneAsync(x => x.Id == id);
    }
}