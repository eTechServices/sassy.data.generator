using StackExchange.Redis;
using System.Threading;
using System;
using Microsoft.Extensions.Caching.Memory;

namespace sassy.bulk.DBContext
{
    /// <summary>
    /// In-Memory data storage
    /// </summary>
    public class Stack
    {
        private static IMemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
        private static int Hours = 5;
        /// <summary>
        /// Retrieves a cached value using the specified key.
        /// </summary>
        /// <param name="key">The key of the cached value.</param>
        /// <returns>The cached value, or null if not found.</returns>
        public static object Get(string key)
        {
            return _cache.Get(key);
        }
        /// <summary>
        /// Inserts a value into the cache with the specified key and expiration time.
        /// </summary>
        /// <param name="key">The key for the cached value.</param>
        /// <param name="value">The value to cache.</param>
        /// <param name="hours">The expiration time in hours.</param>
        public static void Insert(string key, object value)
        {
            _cache.Set(key, value, TimeSpan.FromHours(Hours));
        }
        /// <summary>
        /// Clears the cache and creates a new MemoryCache instance.
        /// </summary>
        public static void Dispose()
        {
            _cache.Dispose();
            _cache = new MemoryCache(new MemoryCacheOptions());
        }
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
