using MongoDB.Driver;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoRedisDbEngine.Core.Operations
{
    public class Remove
    {
        IMongoDatabase database;
        IDatabase cache;
        public Remove(IMongoDatabase database, IDatabase cache)
        {
            this.database = database;
            this.cache = cache;
        }
    }
}
