using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Domain.Eums;

namespace TravelCompany.Domain.DTOs
{
    public class SuitableTripDTO
    {

        public int TripId { get; set; }
        public StationStatus StationStatus { get; set; }

        public int StationAOrder { get; set; }
        public int StationAId { get; set; }
        public string StationAName { get; set; } = null!;
        public DateTime DateAndTime { get; set; }
        public DateTime ArrivalDateAndTime { get; set; }

        public string StationBName { get; set; } = null!;
		public int StationBId { get; set; }
		public string RouteName { get; set; } = null!;
    }
}
