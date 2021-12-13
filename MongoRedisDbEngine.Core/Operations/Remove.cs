using MongoDB.Driver;
using Newtonsoft.Json;
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
        public bool DeleteOne<T>(string id)
        {
            string collection = typeof(T).Name;
            string fromcache = cache.StringGet(typeof(T).Name + ":" + id);

            if (!(String.IsNullOrWhiteSpace(fromcache)))
            {
                cache.KeyDelete(typeof(T).Name + ":" + id);
            }

            var collectionfromdb = database.GetCollection<T>(collection);
            var filter = Builders<T>.Filter.Eq("Id", id);
            var result = collectionfromdb.DeleteOne(filter);
            return result.IsAcknowledged;
        }

    }
}
