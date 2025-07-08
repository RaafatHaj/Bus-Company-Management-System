using System.ComponentModel.DataAnnotations;
using UoN.ExpressiveAnnotations.NetCore.Attributes;

namespace Travel_Company_MVC.ViewModels
{
    public class VehicleAvalibilityViewModel
    {

		public int TripId { get; set; }
		public int VehicleId { get; set; }

        public string VehicleModel { get; set; } = null!;
        public string VehicleNumber { get; set; }=null!;
        public DateTime AvalibilityStartDateTime { get; set; }
        public DateTime AvalibilityEndDateTime { get; set; }

        public DateTime MainTripDateTime { get; set; }
        public DateTime MainTripNewDate { get; set; }
        public TimeSpan MainTripNewTime { get; set; }


        public DateTime? ReturnTripDateTime { get; set; }
        public DateTime ReturnTripNewDate { get; set; }
        public TimeSpan ReturnTripNewTime { get; set; }

        public string DepartureStationName { get; set; } = null!;
        public string DistinationStationName { get; set; } = null!;
        public int TripTimeSpanInMinits { get; set; }



	}
}
