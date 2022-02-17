using MongoDB.Driver;
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

        public T Insert<T>(T model)
        {
            string collection = typeof(T).Name;
            var collectionfromdb = database.GetCollection<T>(collection);
            collectionfromdb.InsertOne(model);
            return model;
        }
    }
}
