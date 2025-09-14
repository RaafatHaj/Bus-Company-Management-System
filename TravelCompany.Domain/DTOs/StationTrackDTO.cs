using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Domain.Eums;

namespace TravelCompany.Domain.DTOs
{
	public class StationTrackDTO
	{

		public int TripId { get; set; }
		public int StationOrder {  get; set; }
		public int StationId { get; set; }
		public string StationName { get; set; } = null!;
		public StationStatus Status { get; set; }
		public string? PreviousStation { get; set; }
		public string? NexttStation { get; set; }
		public DateTime ArrivalDateTime {  get; set; }
		public DateTime DepartureDateTime { get; set; }
		public TripStatus TripStatus { get; set; }
		public string RouteName { get; set; } = null!;
		public int EstimatedDistance { get; set; }
		public int ArrivelLateMinutes { get; set; }
		public int DepartureLateMunutes { get; set; }
		public int StationBoarding {  get; set; }

	}
}
