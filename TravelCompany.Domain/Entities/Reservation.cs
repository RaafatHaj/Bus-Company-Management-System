using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCompany.Domain.Entities
{
    public class Reservation
    {

        public int Id { get; set; } 

        public int ScheduledTravelId { get; set; }
        public Trip? ScheduledTravel { get; set; }

        public int StationAId {  get; set; }
        public Station? StationA { get; set; }

        public int StationBId { get; set; }
        public Station? StationB { get; set; }

        public int SeatNumber { get; set; }
        public long SeatCode { get; set; }

        public string PersonId { get; set; } = null!;
        public string PersonName { get; set; }=null!;
        public string PersonPhone { get; set; } = null!;
        public string? PersonEmail { get; set; }
		public bool? PersonGendor { get; set; }
	}
}
