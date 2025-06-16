using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Server.Models
{
    public class Group
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = string.Empty;

        [BsonElement("GroupName")]
        public string GroupName { get; set; }

        [BsonElement("Message")]
        public List<string> Message { get; set; } = new List<string>();

        public Group(string groupName)
        {
            GroupName = groupName;
        }
    }
}
