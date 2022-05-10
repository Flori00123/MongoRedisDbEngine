using MongoRedisDbEngine.Core;
using MongoRedisDbEngine.Models;

namespace MongoRedisDbEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            DbEngine engine = new DbEngine("mongodb://127.0.0.1:2750", "127.0.0.1:7500", "testdb");
            var test = engine.Add.Insert(new Test() { Title = "Hello" });
            string testus = test.Id;
            var testmodel1 = engine.Get.GetOne<Test>(testus);
            var testmodel2 = engine.Get.GetOne<Test>(testus);
            var testmodel3 = engine.Get.GetOne<Test>(testus);

            testmodel3.Title = "Testus";
            engine.Put.PutOne(testmodel3);
            var testmodel4 = engine.Get.GetOne<Test>(testus);

            engine.Remove.DeleteOne(test);
        }
    }
}
