using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoRedisDbEngine.Core.Operations
{
    public class Put
    {
        IMongoDatabase database;
        IDatabase cache;
        public Put(IMongoDatabase database, IDatabase cache)
        {
            this.database = database;
            this.cache = cache;
        }

        public bool PutOne<T>(string id, T replacement)
        {
            string fromcache = cache.StringGet(typeof(T).Name + ":" + id);
            string collection = typeof(T).Name;

            if (!(String.IsNullOrWhiteSpace(fromcache)))
            {
                cache.KeyDelete(typeof(T).Name + ":" + id);
            }

            var collectionfromdb = database.GetCollection<T>(collection);
            var filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
            var result = collectionfromdb.ReplaceOne(filter, replacement);
            cache.StringSet(typeof(T).Name + ":" + id, JsonConvert.SerializeObject(replacement));
            return result.IsAcknowledged;
        }
    }
}
