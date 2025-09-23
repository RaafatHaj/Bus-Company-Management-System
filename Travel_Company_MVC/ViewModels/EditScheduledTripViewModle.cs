using System.ComponentModel.DataAnnotations;
using TravelCompany.Domain.Const;

namespace Travel_Company_MVC.ViewModels
{
	public class EditScheduledTripViewModle
	{

		public int RouteId { get; set; }
		public int TripTimeSpanInMInits { get; set; }

        public int TripId { get; set; }


		public DateTime MainTripOldDateTime { get; set; }

		[Display(Name = "Main Trip Date"), Required(ErrorMessage = Errors.RequiredWithMessage)]
		public DateTime MainTripNewDate { get; set; }

		[Display(Name = "Main Trip Time"), Required(ErrorMessage = Errors.RequiredWithMessage)]
		public TimeSpan MainTripNewTime { get; set; }

		public bool MainTripHasBookedSeats { get; set; }
		public int MainTripStationStopMinutes { get; set; }
		public bool MainTripHasBreak { get; set; }
		public int? MainTripBreakMinutes { get; set; }
		public int? MainTripStationOrderNextToBreak { get; set; }


		public int? ReturnRouteId { get; set; }
		public int? ReturnTripId { get; set; }


		public DateTime? ReturnTripOldDateTime { get; set; }


		[Display(Name = "Return Trip Date"), Required(ErrorMessage = Errors.RequiredFiled)]
		public DateTime ReturnTripNewDate { get; set; }

		[Display(Name = "Return Trip Date"), Required(ErrorMessage = Errors.RequiredFiled)]
		public TimeSpan ReturnTripNewTime { get; set; }


		public bool ReturnTripHasBookedSeats { get; set; }
		public int  ReturnTripStationStopMinutes { get; set; }
		public bool ReturnTripHasBreak { get; set; }
		public int?  ReturnTripBreakMinutes { get; set; }
		public int?  ReturnTripStationOrderNextToBreak { get; set; }
				

		public int? VehicleId { get; set; }

		public bool IsCustomTripRow { get; set; } = false;

		public bool IsReturnTrip { get; set; } = false;

    }
}
