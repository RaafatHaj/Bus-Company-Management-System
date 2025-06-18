using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Domain.Eums;

namespace TravelCompany.Domain.DTOs
{
    public class ScheduledTripDTO
    {

        public int TripId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int DepartureStationId { get; set; }
        public int TripTimeSpanInMInits { get; set; }
        public TripStatus? Status { get; set; }


        public int? ReturnTripId { get; set; }
        public DateTime? ReturnDate { get; set; }
        public TimeSpan? ReturnTime { get; set; }
        public TripStatus? ReturnStatus { get; set; }

        public int? VehicleId { get; set; }


    }
}
