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

        public int TripId {  get; set; }
        public int VehicleId { get; set; }
        public DateTime MainTripDateTime { get; set; }
        public DateTime ReturnTripDateTime { get; set; }


    }
}
