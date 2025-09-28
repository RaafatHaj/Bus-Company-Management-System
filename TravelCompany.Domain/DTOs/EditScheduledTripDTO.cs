using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Domain.Const;

namespace TravelCompany.Domain.DTOs
{
	public class EditScheduledTripDTO
	{

		public int RouteId { get; set; }
		public int TripId { get; set; }
		public int? VehicleId { get; set; }

		public DateTime MainTripOldDateTime { get; set; }

		public DateTime MainTripNewDate { get; set; }

		public TimeSpan MainTripNewTime { get; set; }

		public bool MainTripHasBookedSeats { get; set; }
		public int MainTripStationStopMinutes { get; set; }
		public bool MainTripHasBreak { get; set; }
		public int? MainTripBreakMinutes { get; set; }
		public int? MainTripStationOrderNextToBreak { get; set; }


		public int? ReturnRouteId { get; set; }
		public int? ReturnTripId { get; set; }


		public DateTime? ReturnTripOldDateTime { get; set; }


		public DateTime ReturnTripNewDate { get; set; }

		public TimeSpan ReturnTripNewTime { get; set; }


		public bool ReturnTripHasBookedSeats { get; set; }
		public int ReturnTripStationStopMinutes { get; set; }
		public bool ReturnTripHasBreak { get; set; }
		public int? ReturnTripBreakMinutes { get; set; }
		public int? ReturnTripStationOrderNextToBreak { get; set; }


        public bool IsReturnTrip { get; set; } = false;







    }
}
