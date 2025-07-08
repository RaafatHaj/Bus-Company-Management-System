using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCompany.Domain.DTOs
{
    public class AssignVehicleDTO
    {

		// TripID
		// VehicleId
		// MainTripDateAndTime
		// ReturnTripDateAndTime 		 
		// MainTripCurrentDateTime datetime,
		// ReturnTripCurrentDateTime datetime,

        public int TripId {  get; set; }
        public int VehicleId { get; set; }
        public DateTime MainTripDateTime { get; set; }
        public DateTime MainTripNewDateTime { get; set; }
        public DateTime? ReturnTripDateTime { get; set; }
        public DateTime ReturnTripNewDateTime { get; set; }
        public int TripTimeSpanInMInits { get; set; }



	}
}
