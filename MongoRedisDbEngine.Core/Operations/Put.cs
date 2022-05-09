using MongoDB.Bson;
using MongoDB.Driver;
using MongoRedisDbEngine.Core.Models;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;

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

        public bool PutOne<T>(T model) where T : DbTable
        {
            string fromcache = cache.StringGet(typeof(T).Name + ":" + model.Id);
            string collection = typeof(T).Name;

            if (!(String.IsNullOrWhiteSpace(fromcache)))
            {
                cache.KeyDelete(typeof(T).Name + ":" + model.Id);
            }

            var collectionfromdb = database.GetCollection<T>(collection);
            var filter = Builders<T>.Filter.Eq("_id", new ObjectId(model.Id));
            var result = collectionfromdb.ReplaceOne(filter, model);
            cache.StringSet(typeof(T).Name + ":" + model.Id, JsonConvert.SerializeObject(model));
            return result.IsAcknowledged;
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

        public void PutMany<T>(List<T> models) where T : DbTable
        {
            foreach(var model in models)
            {
                PutOne(model);
            }
        }

        public void PutMany<T>(Dictionary<string, T> replacements)
        {
            foreach(var replacement in replacements)
            {
                PutOne(replacement.Key, replacement.Value);
            }
        }
    }
}
