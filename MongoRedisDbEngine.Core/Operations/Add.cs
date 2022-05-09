using System.Collections.Generic;
using MongoDB.Driver;
using MongoRedisDbEngine.Core.Models;
using Newtonsoft.Json;
using StackExchange.Redis;

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

        public T Insert<T>(T model, bool cacheEntity = false) where T : DbTable
        {
            string collection = typeof(T).Name;
            var collectionfromdb = database.GetCollection<T>(collection);
            collectionfromdb.InsertOne(model);
            if (cacheEntity)
                cache.StringSet(typeof(T).Name + ":" + model.Id, JsonConvert.SerializeObject(model));
            return model;
        }

        public List<T> Insert<T>(List<T> models, bool cacheEntitys = false) where T : DbTable
        {
            List<T> modelsFromDb = new List<T>();
            foreach(T model in models)
            {
                modelsFromDb.Add(Insert(model, cacheEntitys));
            }
            return modelsFromDb;
        }
    }
}
