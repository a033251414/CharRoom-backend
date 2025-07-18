using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Server.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("userName")]
        public string UserName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
        //jwt
        public string Token { get; set; } = string.Empty;
    }
}