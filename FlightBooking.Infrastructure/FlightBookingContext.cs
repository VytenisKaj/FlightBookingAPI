using FlightBooking.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightBooking.Infrastructure
{
    public class FlightBookingContext : DbContext
    {
        public FlightBookingContext(DbContextOptions<FlightBookingContext> options) : base(options) { }

        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Airplane> Airplanes { get; set; }
        public DbSet<FlightDetails> Flights { get; set; }
    }
}
