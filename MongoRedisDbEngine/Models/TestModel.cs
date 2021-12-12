using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoRedisDbEngine.Models
{
    public class TestModel
    {
        public ObjectId Id { get; set; }
        public string Title { get; set; }
    }
}
