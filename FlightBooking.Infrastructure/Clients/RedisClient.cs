using StackExchange.Redis;

namespace FlightBooking.Infrastructure.Clients
{
    public class RedisClient : IRedisClient
    {
        private readonly ConfigurationOptions _configuration;
        private readonly Lazy<IConnectionMultiplexer> _connection;

        public RedisClient(string connection, bool allowAdmin = false)
        {
            _configuration = new ConfigurationOptions()
            {
                EndPoints = { connection },
                AllowAdmin = allowAdmin,
                ClientName = "Redis Client",
                ReconnectRetryPolicy = new LinearRetry(5000),
                AbortOnConnectFail = false,
            };
            _connection = new Lazy<IConnectionMultiplexer>(() =>
            {
                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(_configuration);
                return redis;
            });
        }

        public IConnectionMultiplexer Connection { get { return _connection.Value; } }

        public IDatabase Database => Connection.GetDatabase();
    }
}
