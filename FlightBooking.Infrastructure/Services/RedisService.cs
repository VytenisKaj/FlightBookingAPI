using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace FlightBooking.Infrastructure
{
    public class RedisService : IRedisService
    {
        private readonly IDatabase _database;
        //private readonly ILogger _logger;

        public RedisService(IDatabase database) 
        {
            _database = database;
        }

        public string GetItemByKey(string key)
        {
            var keyExists = _database.KeyExists(key);

            if (keyExists)
            {
                return _database.StringGet(key).ToString();
            }

            //_logger.LogError("Entered key does not exist.");
            return string.Empty;
        }

        public void AddItem(KeyValuePair<string, string> keyValue)
        {
            _database.StringSet(keyValue.Key, keyValue.Value);
        }

        public void UpdateItem(KeyValuePair<string, string> keyValue)
        {
            var keyValuePairExists = _database.KeyExists(keyValue.Key);

            if (keyValuePairExists)
            {
                _database.StringSet(keyValue.Key, keyValue.Value);
            }
        }

        public void DeleteKey(string key)
        {
            var keyExists = _database.KeyExists(key);

            if (keyExists)
            {
                _database.KeyDelete(key);
            }
        }
    }
}
