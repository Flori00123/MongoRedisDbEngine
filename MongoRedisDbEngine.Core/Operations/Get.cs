﻿using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoRedisDbEngine.Core.Operations
{
    public class Get
    {
        IMongoDatabase database;
        IDatabase cache;
        public Get(IMongoDatabase database, IDatabase cache)
        {
            this.database = database;
            this.cache = cache;
        }

        public T GetOne<T> (string id)
        {
            string collection = typeof(T).Name;
            string fromcache = cache.StringGet(typeof(T).Name + ":" + id);

            if(String.IsNullOrWhiteSpace(fromcache))
            {
                var collectionfromdb = database.GetCollection<T>(collection);
                var filter = Builders<T>.Filter.Eq("_id", new ObjectId(id));
                var result = collectionfromdb.Find(filter).ToList();
                if(result.Count > 1)
                {
                    throw new Exception("More than one on one key!");
                }
                cache.StringSet(typeof(T).Name + ":" + id, JsonConvert.SerializeObject(result[0]));
                return result[0];
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(fromcache);
            }
        }

        public List<T> GetAll<T> ()
        {
            string collection = typeof(T).Name;
            var collectionfromdb = database.GetCollection<T>(collection);
            return collectionfromdb.Find(Builders<T>.Filter.Empty).ToList();
        }
    }
}
