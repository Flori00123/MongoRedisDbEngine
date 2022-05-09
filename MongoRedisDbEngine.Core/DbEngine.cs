using MongoDB.Bson;
using MongoDB.Driver;
using MongoRedisDbEngine.Core.Operations;
using StackExchange.Redis;
using System.Collections.Generic;

namespace MongoRedisDbEngine.Core
{
    public class DbEngine
    {
        public Add Add { get; set; }
        public Get Get { get; set; }
        public Put Put { get; set; }
        public Remove Remove { get; set; }

        internal IMongoDatabase database { get; set; }
        internal IDatabase cache { get; set; }

        public DbEngine(string mongoConnectionString, string redisConnectionString, string database)
        {
            //MongoInit
            MongoClient dbClient = new MongoClient(mongoConnectionString);
            this.database = dbClient.GetDatabase(database);

            //RedisInit
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(redisConnectionString);
            this.cache = redis.GetDatabase();

            //EngineInit
            Add = new Add(this.database, this.cache);
            Get = new Get(this.database, this.cache);
            Put = new Put(this.database, this.cache);
            Remove = new Remove(this.database, this.cache);
        }
    }
}
