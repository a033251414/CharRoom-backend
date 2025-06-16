using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Server.Models
{
    public class Message
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }  

        [BsonElement("groupId")]
        public string? GroupId { get; set; }  //群組ID

        [BsonElement("userName")]
        public string? UserName { get; set; }  //使用者名字

        [BsonElement("content")]
        public string? Content { get; set; }  //訊息內容

        [BsonElement("replyToMessageId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ReplyToMessageId { get; set; } //回覆的該訊息

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
    }
}
