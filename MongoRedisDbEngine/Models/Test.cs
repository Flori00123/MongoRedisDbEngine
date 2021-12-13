﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoRedisDbEngine.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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