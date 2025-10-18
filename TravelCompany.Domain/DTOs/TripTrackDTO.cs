using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelCompany.Domain.Eums;

namespace TravelCompany.Domain.DTOs
{
	public class TripTrackDTO
	{
		public int TripId { get; set; }
		public int StationOrder { get; set; }
		public int StationId { get; set; }
		public string StationName { get; set; } = null!;
		public StationStatus Status { get; set; }
		public DateTime ArrivalDateTime { get; set; }
		public int ArrivalLateMinutes { get; set; }
		public DateTime DepartureDateTime { get; set; }
		public int DepartureLateMinutes { get; set; }


	}
}
