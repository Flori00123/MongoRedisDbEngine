﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoRedisDbEngine.Models
{
    public class Test
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Title { get; set; }
    }
}