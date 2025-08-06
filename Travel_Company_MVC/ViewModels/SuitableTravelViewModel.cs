using Microsoft.AspNetCore.Mvc.Rendering;
using TravelCompany.Domain.Eums;

namespace Travel_Company_MVC.ViewModels
{
    public class SuitableTravelViewModel
    {

        public int TripId { get; set; }
        public StationStatus StationStatus { get; set; }

        public int StationAOrder { get; set; }
		public int StationAId { get; set; }
		public string StationAName { get; set; } = null!;
        public DateTime DateAndTime{ get; set; }
		public DateTime ArrivalDateAndTime { get; set; }

		public string StationBName { get; set; } = null!;
		public int StationBId { get; set; }
		public string RouteName { get; set;} = null!;
    }
}
