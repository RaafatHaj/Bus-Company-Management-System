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
        public Trip? ScheduledTrip { get; set; }

        public int VehicleId { get; set; }
        public Vehicle? Vehicle { get; set; }

        public int? DriverId { get; set; }
        public Driver? Driver { get; set; }


        public DateTime StartDateAndTime { get; set; }
        public DateTime EndDateAndTime { get; set; }


        public int StopedStationId { get; set; }
        public Station? StopedStation { get; set; }



    }
}
