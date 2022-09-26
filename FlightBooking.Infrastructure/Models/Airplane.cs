namespace FlightBooking.Infrastructure.Models
{
    public class Airplane
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Capacity { get; set; }
        public List<DateTime> FlightTimes { get; set; }
    }
}
