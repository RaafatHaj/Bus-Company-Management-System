using TravelCompany.Domain.Eums;

namespace TravelCompany.Domain.Entities
{
	public class ActiveTrip	
	{

		public int TripId { get; set; }
		public Trip? Trip { get; set; }
		public int StationOrder {  get; set; }
		public int StationId { get; set;}
		public string StationName { get; set; } = null!;
		public StationStatus StationStatus { get; set; }
		public TimeSpan ArrivalTime { get; set; }
		public TimeSpan DepartureTime { get; set; }
		public TimeSpan? ActualArrivalTime { get; set; }
		public TimeSpan? ActualDepartureTime { get; set; }


	}
}
