using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Server.Services
{
    public class GroupService
    {
        private readonly IMongoCollection<Group> _groupCollection;

        public GroupService(IOptions<MongoDBSettings> mongoSettings)
        {
            var mongoClient = new MongoClient(mongoSettings.Value.ConnectionString);
            var database = mongoClient.GetDatabase(mongoSettings.Value.DatabaseName);
            _groupCollection = database.GetCollection<Group>("Group");
        }

        public async Task<List<Group>> GetAsync() =>
            await _groupCollection.Find(_ => true).ToListAsync();

        public async Task<Group?> GetAsync(string id) =>
            await _groupCollection.Find(g => g.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Group newGroup) =>
            await _groupCollection.InsertOneAsync(newGroup);

   

        public async Task RemoveAsync(string id) =>
            await _groupCollection.DeleteOneAsync(g => g.Id == id);
    }
}
