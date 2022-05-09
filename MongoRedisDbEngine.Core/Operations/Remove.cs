using MongoDB.Driver;
using MongoRedisDbEngine.Core.Models;
using StackExchange.Redis;
using System;
using System.Collections.Generic;

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
            var filter = Builders<T>.Filter.Eq("_id", id);
            var result = collectionfromdb.DeleteOne(filter);
            return result.IsAcknowledged;
        }

        public bool DeleteOne<T>(T model) where T : DbTable
        {
            string collection = typeof(T).Name;
            string fromcache = cache.StringGet(typeof(T).Name + ":" + model.Id);

            if (!(String.IsNullOrWhiteSpace(fromcache)))
            {
                cache.KeyDelete(typeof(T).Name + ":" + model.Id);
            }

            var collectionfromdb = database.GetCollection<T>(collection);
            var filter = Builders<T>.Filter.Eq("_id", model.Id);
            var result = collectionfromdb.DeleteOne(filter);
            return result.IsAcknowledged;
        }

        public void DeleteMany<T>(List<string> ids)
        {
            foreach (string id in ids)
            {
                DeleteOne<T>(id);
            }
        }

        public void DeleteMany<T>(List<T> models) where T : DbTable
        {
            foreach (T model in models)
            {
                DeleteOne(model);
            }
        }
    }
}
