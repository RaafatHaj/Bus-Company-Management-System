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
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<TripPattern> TripPatterns { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<TripAssignment> TripAssignments { get; set; }
        public DbSet<ActiveTrip> ActiveTrips { get; set; }
        public DbSet<ApplicationConst> ApplicationConsts { get; set; }
        

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<RoutePoint>().HasKey(e => new {e.RouteId,e.PointId});
            builder.Entity<ActiveTrip>().HasKey(e => new {e.TripId,e.StationOrder});


            builder.Entity<TripPattern>()
               .HasIndex(p => new { p.RouteId , p.Time })
               .IsUnique();

            //	builder.Entity<Travel>()
            // .Property(o => o.ScheduleDuration)
            // .HasConversion<int>();

            //	builder.Entity<Travel>()
            //.Property(o => o.ScheduleType)
            //.HasConversion<int>();

            builder.Entity<Trip>()
	    	.HasOne(t => t.TripAssignment)
	    	.WithOne(ta => ta.Trip)
	    	.HasForeignKey<TripAssignment>(ta => ta.TripId)
	    	.OnDelete(DeleteBehavior.Restrict); // optional

			//builder.Entity<TripPattern>()
		 //   .Property(e => e.StartDate)
		 //   .HasColumnType("date");

   //         builder.Entity<TripPattern>()
   //         .Property(e => e.EndDate)
   //         .HasColumnType("date");

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


		

			//nvarchar(MAX)

			//builder.Entity<ScheduledTravel>()
			//         .Property(e => e.Date)
			//         .HasColumnType("date");


			base.OnModelCreating(builder);
        }

    }
}
