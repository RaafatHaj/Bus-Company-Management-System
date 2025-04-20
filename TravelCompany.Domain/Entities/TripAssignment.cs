using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCompany.Domain.Entities
{
    public class TripAssignment
    {
        public int Id { get; set; }
        public int ScheduledTripId { get; set; }
        public ScheduledTrip? ScheduledTrip { get; set; }

        public int VehicleId { get; set; }
        public Vehicle? Vehicle { get; set; }

        public int? DriverId { get; set; }
        public Driver? Driver { get; set; }

        public DateTime Date { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }


    }
}
