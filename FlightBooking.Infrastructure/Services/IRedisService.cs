namespace FlightBooking.Infrastructure
{
    public interface IRedisService
    {
        string GetItemByKey(string key);
        void AddItem(KeyValuePair<string, string> keyValue);
        void UpdateItem(KeyValuePair<string, string> keyValue);
        void DeleteKey(string key);
    }
}
