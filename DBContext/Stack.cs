using StackExchange.Redis;
using System.Threading;
using System;

namespace sassy.bulk.DBContext
{
    public class Stack
    {
        public static bool Insert(string key, string value) 
        {
            var db = Database();
            return db.StringSet(key, value);
        }
        public static string Get(string key) => Database().StringGet(key);
        

        private static IDatabase Database()
        {
            var connectionString = "localhost,AbortConnect=false"; 
            var retryCount = 0;
            const int maxRetries = 5;
            const int initialWaitMs = 100; 

            while (retryCount < maxRetries)
            {
                try
                {
                    var redis = ConnectionMultiplexer.Connect(connectionString);
                    return redis.GetDatabase();
                }
                catch (RedisConnectionException ex)
                {
                    Console.WriteLine($"Redis connection failed ({retryCount + 1}/{maxRetries}): {ex.Message}");
                    retryCount++;
                    Thread.Sleep(initialWaitMs * (int)Math.Pow(2, retryCount)); 
                }
            }
            throw new System.Exception("Failed to connect to Redis after multiple attempts.");
        }
    }
}
