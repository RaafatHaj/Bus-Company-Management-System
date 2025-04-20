using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelCompany.Domain.Entities
{
    public class route
    {

        public int RouteId { get; set; }
        public string RouteName { get; set; } = null!;
        ICollection<Trip> Trips { get; set; } = new List<Trip>();

        public int ?FirstStationId { get; set; }
        public Station? FirstStation { get; set; }

        public int? LastStationId { get; set; }
        public Station? LastStation { get; set; }

        public int? StationsNumber { get; set; }
        public int? ReverseRouteId { get; set; }
        public int EstimatedTime { get; set; }
    }
}
