using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Domain.Eums;

namespace TravelCompany.Domain.DTOs
{
    public class ScheduledTripBaseDTO
    {


        public int TripId { get; set; }
        public string RouteName { get; set; } = null!;
        public DateTime Date { get; set; }
        public TimeSpan Time { get; set; }
        public int DepartureStationId { get; set; }
        public int TripTimeSpanInMInits { get; set; }
        public TripStatus Status { get; set; }  
        public int? MainTripId { get; set; }

        public int? VehicleId { get; set; } 
        public string? VehicleNumber { get; set; }
        public string? VehicleModel { get; set; }   

        public int RouteId { get; set; }
        public int? Seats { get; set; }
        public bool HasBookedSeat { get; set; }

        public int ArrivedStationOrder { get; set; }
    }
}
