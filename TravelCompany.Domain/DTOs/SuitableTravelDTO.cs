using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Domain.Eums;

namespace TravelCompany.Domain.DTOs
{
    public class SuitableTravelDTO
    {

        public int ScheduledTravelID { get; set; }
        public StationStatus Status { get; set; }


        public int StationAOrder {  get; set; }
        public int StationAId { get; set; }
        public string StationAName { get; set; } = null!;
        public DateTime StationAArrivalDateAndTime { get; set; }



        public int StationBOrder { get; set; }
        public int StationBId { get; set; }
        public string StationBName { get; set; } = null!;
        public DateTime StationBArrivalDateAndTime { get; set; }


        public int AvailableSeats { get; set; }
        public int BookedSeats { get; set; }
     
        public int RouteStationsNumber { get; set; }
    }
}
