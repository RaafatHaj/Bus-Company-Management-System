using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Domain.Entities;
using TravelCompany.Domain.Eums;

namespace TravelCompany.Domain.DTOs
{
    public class ScheduledTravelDetailDTO
    {


        public int ScheduledTravelId { get; set; }
        public int StationOrder { get; set; }
        public int StationId { get; set; }
        public string StationName { get; set; } = null!;
        public StationStatus Status { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public DateTime Date {  get; set; }
        public int AvailableSeats { get; set; }
        public int BookedSeates { get; set; }


    }
}
