using StackExchange.Redis;

namespace FlightBooking.Infrastructure.Clients
{
    public interface IRedisClient
    {
        IConnectionMultiplexer Connection { get; }
        IDatabase Database { get; }
    }
}
