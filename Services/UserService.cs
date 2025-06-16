using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Server.Models;
using System.Collections.Generic;

namespace Server.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IOptions<MongoDBSettings> mongoSettings)
        {
                     var mongoClient = new MongoClient(mongoSettings.Value.ConnectionString);
            var database = mongoClient.GetDatabase(mongoSettings.Value.DatabaseName);
            _users = database.GetCollection<User>("Users"); 
        }

        public List<User> Get() => _users.Find(u => true).ToList();

        public User Get(string id) => _users.Find(u => u.Id == id).FirstOrDefault();
        
        public User GetByUserName(string userName)
        {
         return _users.Find(u => u.UserName == userName).FirstOrDefault();
        }

        public User Create(User user)
        {
            var existingUser = _users.Find(u => u.UserName == user.UserName).FirstOrDefault();

            if (existingUser != null)
            {
                throw new InvalidOperationException("該名稱已使用");
            }
            else
            {
                _users.InsertOne(user);
                return user;
            }

        }
        public void UpdateToken(string userId, string token)
        {
          var update = Builders<User>.Update.Set(u => u.Token, token);
          _users.UpdateOne(u => u.Id == userId, update);
        }

      
        public void Delete(string id) => _users.DeleteOne(u => u.Id == id);
    }
}
