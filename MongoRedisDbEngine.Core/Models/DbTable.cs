using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoRedisDbEngine.Core.Models
{
    public abstract class DbTable
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
    }
}
