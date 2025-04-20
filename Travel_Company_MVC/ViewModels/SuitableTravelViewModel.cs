using Microsoft.AspNetCore.Mvc.Rendering;
using TravelCompany.Domain.Eums;

namespace Travel_Company_MVC.ViewModels
{
    public class SuitableTravelViewModel
    {

        public int ScheduledTravelID { get; set; }
        public StationStatus Status { get; set; }

        public int StationAOrder { get; set; }
        public string StationAName { get; set; } = null!;
        public DateTime Date{ get; set; }
        public TimeSpan Time { get; set; }


        public int StationBOrder { get; set; }
        public string StationBName { get; set; } = null!;


        public int AvailableSeats { get; set; }
        public int BookedSeats { get; set; }

        public int RouteStationsNumber { get; set; }
    }
}
