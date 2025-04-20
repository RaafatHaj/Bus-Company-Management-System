using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Domain.Eums;

namespace TravelCompany.Domain.Entities
{
    public class ScheduledTrip
    {
        public int Id {  get; set; }
        public DateTime DateAndTime { get; set; }

        // Modify it to Enum ...
        public TripStatus status { get; set; }
        public int TripId { get; set; }
        public Trip? Trip { get; set; }

		//public TimeSpan TravelTime { get; set; }
		public int Seats { get; set; }
        public bool HasBookedSeat { get; set; }
        public long StatusCode { get; set; }

        //public int? DriverId { get; set; }
        //public Driver? Driver { get; set; }

        //public int? VehicleId { get; set; }
        //public Vehicle? Vehicle { get; set; }

        public bool IsIrregular { get; set; }
        public int? ReverseScheduledTripId { get; set; }



        public ICollection<TravelStation> Details { get; set; } =new List<TravelStation>();

	}
}
