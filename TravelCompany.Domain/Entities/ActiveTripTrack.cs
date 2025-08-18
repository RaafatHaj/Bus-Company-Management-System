using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Domain.Eums;

namespace TravelCompany.Domain.Entities
{
	public class ActiveTripTrack
	{

		public int TripId { get; set; }
		public Trip? Trip { get; set; }
		public int StationOrder { get; set; }
		public int StationId { get; set; }
		public string StationName { get; set; } = null!;
		public StationStatus Status { get; set; }
		public string? PreviousStation { get; set; }
		public string? NexttStation { get; set; }

		public DateTime PlannedArrivalDateTime { get; set; }
		public DateTime PlannedDepartureDateTime { get; set; }
		public DateTime ActualArrivalDateTime { get; set; }
		public DateTime ActualDepartureDateTime { get; set; }
		public TripStatus TripStatus { get; set; }

		public int RouteId { get; set; }
		public route? Route { get; set; }
		public string RouteName { get; set; } = null!;
		public int EstimatedDistance { get; set; }


	}
}
