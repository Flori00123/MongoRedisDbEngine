using MongoDB.Driver;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoRedisDbEngine.Core.Operations
{
    public class Add
    {
        IMongoDatabase database;
        IDatabase cache;
        public Add(IMongoDatabase database, IDatabase cache)
        {
            this.database = database;
            this.cache = cache;
        }

        public void Insert<T>(string collection, T model)
        {
            var collectionfromdb = database.GetCollection<T>(collection);
            collectionfromdb.InsertOne(model);
        }
    }
}
