using FlightBooking.Infrastructure.Models;
using Microsoft.Extensions.Logging;

namespace FlightBooking.Infrastructure
{
    public class DataSeeder
    {
        public DataSeeder() { }

        public static void SeedAirplanes(FlightBookingContext context)
        {
            //var logger = LoggerFactory.Create(options => { }).CreateLogger<DataSeeder>();

            if (!context.Airplanes.Any())
            {
                var defaultAvailableFlightTimes = new List<DateTime>()
                {
                    new DateTime(2022, 9, 25, 8, 0, 0),
                    new DateTime(2022, 9, 30, 10, 0, 0),
                    new DateTime(2022, 10, 1, 7, 30, 0),
                    new DateTime(2022, 10, 6, 5, 45, 0),
                    new DateTime(2022, 10, 8, 10, 55, 0)
                };

                var airplanes = new List<Airplane>
                {
                    new() { Name = "WizzAir", Capacity = 200, FlightTimes = defaultAvailableFlightTimes },
                    new() { Name = "RyaAir", Capacity = 150, FlightTimes = defaultAvailableFlightTimes },
                    new() { Name = "AirBaltic", Capacity = 100, FlightTimes = defaultAvailableFlightTimes }
                };

                context.Airplanes.AddRange(airplanes);
                context.SaveChanges();

                //logger.LogInformation("'SeedAirplanes' data seeding finished successfully.");
            }

            //logger.LogError("'context.Aiplanes' already has values. 'SeedAirplanes' was skipped.");
        }
    }
}
