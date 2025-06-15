using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCompany.Domain.Entities
{
    public class Vehicle
    {
         

        public int VehicleId { get; set; }
        public int Seats { get; set; }
        public string Type { get; set; } = null!;
        public bool IsActive { get; set; }
        public int StationId { get; set; }
        public Station? Station { get; set; }
        public string VehicleNumber { get; set; } = null!;

        public ICollection<TripAssignment> Assignments { get; set; } = new List<TripAssignment>();

      //  public ICollection<ScheduledTrip> ScheduledTrips { get; set; } = new List<ScheduledTrip>();

    }
}
