using TravelCompany.Domain.Eums;

namespace TravelCompany.Domain.Entities
{
    public class Trip
    {

        public int TripId {  get; set; }
        public int RouteId { get; set; }
        public route? Route { get; set; }

        // [Column(TypeName = "time(7)")] // Specify SQL Server column type [Column(TypeName = "time(7)")] // Specify SQL Server column type
        public TimeSpan TripTime { get; set; }

       

        public ICollection<ScheduledTrip> ScheduledTrips { get; set;}=new List<ScheduledTrip>();
        public ICollection<Recurring> Recurrings { get; set;}=new List<Recurring>();
      


  
       
    }
}
