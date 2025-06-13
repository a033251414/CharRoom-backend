using MongoDB.Driver;
using Microsoft.Extensions.Options;
using Server.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Server.Services
{
    public class MessageService
    {
        private readonly IMongoCollection<Message> _messages;

        public MessageService(IOptions<MongoDBSettings> mongoSettings)
        {
            var mongoClient = new MongoClient(mongoSettings.Value.ConnectionString);
            var database = mongoClient.GetDatabase(mongoSettings.Value.DatabaseName);
            _messages = database.GetCollection<Message>("Messages");
        }

        public async Task CreateMessageAsync(Message message)
        {
            await _messages.InsertOneAsync(message);
        }

        public async Task<List<Message>> GetMessagesByGroupIdAsync(string groupId)
        {
            return await _messages
            .Find(msg => msg.GroupId == groupId)
            .SortBy(msg => msg.CreatedAt)
            .ToListAsync();
        }

         public async Task<bool> SetMessageToNullAsync(string id)
        {
          var filter = Builders<Message>.Filter.Eq(m => m.Id, id);
          var update = Builders<Message>.Update.Set(m => m.Content, null);
          var result = await _messages.UpdateOneAsync(filter, update);
         return result.ModifiedCount > 0;
        }
        

    }
}
