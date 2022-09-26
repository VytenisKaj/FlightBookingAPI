using FlightBooking.Infrastructure;
using FlightBooking.Infrastructure.Clients;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace FlightBooking.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedisController : ControllerBase
    {
        private readonly IDatabase _database;
        private readonly IRedisClient _redisClient;
        private readonly IRedisService _redisService;

        public RedisController(IDatabase database, IRedisClient redisClient, IRedisService redisService)
        {
            _database = database;
            _redisClient = redisClient;
            _redisService = redisService;
        }

        // GET: api/redis/allItems
        [HttpGet("/allItems")]
        public List<KeyValuePair<string, string>> GetAllDatabaseItems()
        {
            var keys = _redisClient.Connection.GetServer(_redisClient.Connection.GetEndPoints().First()).Keys();
            var keyList = keys.Select(x => x.ToString()).ToList();

            if (keyList == null)
            {
                return new List<KeyValuePair<string, string>>();
            }

            var result = new List<KeyValuePair<string, string>>();

            foreach (var key in keyList)
            {
                var value = _database.StringGet(key).ToString() ?? "";
                result.Add(new(key, value));
            }

            return result;
        }

        // GET api/redis/key
        [HttpGet]
        public string GetValue([FromQuery] string key)
        {
            return _redisService.GetItemByKey(key);
        }

        // POST api/redis
        [HttpPost]
        public void AddItem([FromBody] KeyValuePair<string, string> keyValue)
        {
            _redisService.AddItem(keyValue);
        }

        // PUT api/redis/
        [HttpPut("{key}")]
        public void UpdateItem([FromBody] KeyValuePair<string, string> keyValue)
        {
            _redisService.UpdateItem(keyValue);
        }

        // DELETE api/redis/key
        [HttpDelete("{key}")]
        public void DeleteKey(string key)
        {
            _redisService.DeleteKey(key);
        }
    }
}
