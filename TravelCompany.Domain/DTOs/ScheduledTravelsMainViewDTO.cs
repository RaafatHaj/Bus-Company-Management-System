using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Domain.Eums;

namespace TravelCompany.Domain.DTOs
{
    public class ScheduledTravelsMainViewDTO
    {

        public int ScheduledTravelId { get; set; }
        public DateTime Date { get; set; }
        public TimeSpan TravelTime { get; set; }
        public string RouteName { get; set; } = null!;
        public int Seats { get; set; }
        public TripStatus Status { get; set; }
        public string FirstStation { get; set; }=null!;
        public string LastStation { get; set; } = null!;



    }
}
