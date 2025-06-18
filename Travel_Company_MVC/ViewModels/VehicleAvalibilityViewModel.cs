using System.ComponentModel.DataAnnotations;
using UoN.ExpressiveAnnotations.NetCore.Attributes;

namespace Travel_Company_MVC.ViewModels
{
    public class VehicleAvalibilityViewModel
    {


        public int VehicleId { get; set; }

        public string? VehicleModel { get; set; } = null!;
        public string? VehicleNumber { get; set; }=null!;
        public DateTime? AvalibilityStartDateAndTime { get; set; }
        public DateTime? AvalibilityEndDateAndTime { get; set; }

        public DateTime? MainTripStartDateAndTime { get; set; }
        public DateTime? ReturnTripStartDateAndTime { get; set; }
        public DateTime? MainTripEndDateAndTime { get; set; }
        public DateTime? ReturnTripEndDateAndTime { get; set; }


        public int TripId { get; set; }
        public int? TripTimeSpanInMinits { get; set; }
        public string? DepartureStationName { get; set; } = null!;
        public string? DistinationStationName { get; set; } = null!;


        public DateTime? MainTripDate { get; set; }
        public DateTime? ReturnTripDate { get; set; }
        public TimeSpan? MainTripTime { get; set; }
        public TimeSpan? ReturnTripTime { get; set; }

        // TripID
        // VehicleId
        // MainTripDateAndTime
        // ReturnTripDateAndTime

    }
}
