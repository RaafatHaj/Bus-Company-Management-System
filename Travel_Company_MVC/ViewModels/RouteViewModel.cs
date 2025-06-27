using TravelCompany.Domain.Entities;

namespace Travel_Company_MVC.ViewModels
{
    public class RouteViewModel
    {

        public int RouteId { get; set; }
        public string RouteName { get; set; } = null!;

        public int FirstStationId { get; set; }
        public Station? FirstStation { get; set; }

        public int LastStationId { get; set; }
        public Station? LastStation { get; set; }

        public int StationsNumber { get; set; }
        public int ReverseRouteId { get; set; }
        public int EstimatedTime { get; set; }
        public int EstimatedDistance { get; set; }

        //public ICollection<TripPattern> RouteRecurrings { get; set; } = new List<TripPattern>();
        public ICollection<Trip> Trips { get; set; } = new List<Trip>();



    }
}
