using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Server.Models
{
    public class Message
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }  

        [BsonElement("groupId")]
        public string? GroupId { get; set; }  //群組ID

        [BsonElement("userId")]
        public string? UserId { get; set; }  //使用者ID

        [BsonElement("content")]
        public string? Content { get; set; }  //訊息內容

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
    }
}
