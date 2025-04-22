using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using TravelCompany.Domain.Entities;
using TravelCompany.Infrastructure.Persistence.Entities;

namespace TravelCompany.Infrastructure.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Station> Stations { get; set; }
        public DbSet<Point> Points { get; set; }
        public DbSet<route> Routes { get; set; }
        public DbSet<RoutePoint> RoutePoints { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Day> Days { get; set; }
        public DbSet<TravelStation> TravelStations { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Recurring> Recurrings { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<TripAssignment> TripAssignments { get; set; }
        

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<RoutePoint>().HasKey(e => new {e.RouteId,e.PointId});
            builder.Entity<TravelStation>().HasKey(e => new {e.ScheduledTravelId,e.StationOrder});
          

            //	builder.Entity<Travel>()
            // .Property(o => o.ScheduleDuration)
            // .HasConversion<int>();

            //	builder.Entity<Travel>()
            //.Property(o => o.ScheduleType)
            //.HasConversion<int>();

    

			builder.Entity<Recurring>()
		    .Property(e => e.StartDate)
		    .HasColumnType("date");

            builder.Entity<Recurring>()
            .Property(e => e.EndDate)
            .HasColumnType("date");

            builder.Entity<Driver>()
            .Property(e => e.LicenseExpiry)
            .HasColumnType("date");
           
            builder.Entity<Driver>()
            .Property(e => e.HireDate)
            .HasColumnType("date");

            builder.Entity<Driver>()
            .Property(e => e.FirstName)
            .HasColumnType("nvarchar(50)");

            builder.Entity<Driver>()
            .Property(e => e.LastName)
            .HasColumnType("nvarchar(50)");

            builder.Entity<Driver>()
            .Property(e => e.LicenseNumber)
            .HasColumnType("nvarchar(50)");

            builder.Entity<Driver>()
            .Property(e => e.PhoneNumber)
            .HasColumnType("nvarchar(20)");

            builder.Entity<Driver>()
            .Property(e => e.Email)
            .HasColumnType("nvarchar(50)");

            builder.Entity<Schedule>()
            .Property(e => e.Date)
            .HasColumnType("date");

            //nvarchar(MAX)

            //builder.Entity<ScheduledTravel>()
            //         .Property(e => e.Date)
            //         .HasColumnType("date");


            base.OnModelCreating(builder);
        }

    }
}
